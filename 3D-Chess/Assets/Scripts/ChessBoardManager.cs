using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardManager : MonoBehaviour
{
    public static ChessBoardManager Instance { set; get;}
    private bool [,] allowedMoves{ set; get;}

    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedCehessman;

    private const float TileSize = 1.0f;
    private const float TileOffset = 0.5f;

    private int seletionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    //Orjentacija za likove
    private Quaternion blackRot = Quaternion.Euler(0, 180, 0);
    private Quaternion whiteRot = Quaternion.identity;

    private Material previousMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    public bool isWhiteTurn = true;

    private void Start()
    {
        Instance = this;
        SpawnAllChessman();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessboard();

        //Odabir pritiskom misa
        if (Input.GetMouseButtonDown (0)) //(Nula jer je ljevi klik)
        {
            if (seletionX >= 0 && selectionY >= 0)//Provjera jesmo li kliknuli na plocu
            {
                if (selectedCehessman == null)//jesmo li odabrali figuru
                {
                    SelectChessman(seletionX, selectionY);
                }
                //Pomjeranje figure
                else
                {
                    MoveChessman(seletionX, selectionY);
                }
            }
        }
    }

    private void SelectChessman(int x, int y)
    {
        //Nema figura na toj pozicij
        if (Chessmans [x, y] == null)
            return;

        //Provjera boje (Uvjet vrijedi ako za crne figure)
        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;

        //Ako se figura ne moze pomjerit ne odabiri je
        bool hasAtleastOneMove = false;
        allowedMoves = Chessmans [x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                    hasAtleastOneMove = true;
            }
        }

        if (!hasAtleastOneMove)
            return;

        selectedCehessman = Chessmans[x, y];
        //Mjenjanje materijala pri odabiru figure
        previousMat = selectedCehessman.GetComponent<MeshRenderer>().material;
       // selectedMat.mainTexture = previousMat.mainTexture;
        selectedCehessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }


    //kretnja figura
    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Chessman c = Chessmans[x, y];

            //Ako pojedemo figuru
            if (c != null && c.isWhite != isWhiteTurn)
            {
                //Kad pojedemo kralja
                if (c.GetType() == typeof(King))
                {
                    //Kraj igre
                    EndGame();
                    return;
                }

                //Unistavanje figura kad se pojedu
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            //Enpassa
           if (x == EnPassantMove[0] && y == EnPassantMove[1])//Ako smo izveli enpasant
            {
                //Bijeli
                if (isWhiteTurn)
                    c = Chessmans[x, y - 1];
                //Crni
                else
                    c = Chessmans[x, y + 1];

                    activeChessman.Remove(c.gameObject);
                    Destroy(c.gameObject);
            }
            EnPassantMove[0] = -1;//Resetiranje funkcije
            EnPassantMove[1] = -1;

            if (selectedCehessman.GetType() == typeof(Pawn))
            {
                //Promotion
                //Bjeli
                if (y == 7)
                {
                    activeChessman.Remove(selectedCehessman.gameObject);
                    Destroy(selectedCehessman.gameObject);
                    SpawnChessman(1, x, y,whiteRot);
                    selectedCehessman = Chessmans[x, y];
                }
                //Crni
                else if (y == 0)
                {
                    activeChessman.Remove(selectedCehessman.gameObject);
                    Destroy(selectedCehessman.gameObject);
                    SpawnChessman(7, x, y, blackRot);
                    selectedCehessman = Chessmans[x, y];
                }

                //Bjeli
                if (selectedCehessman.CurrentY == 1 && y == 3)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                }
                //Crni
                else if (selectedCehessman.CurrentY == 6 && y == 4)
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                }

            }

            Chessmans [selectedCehessman.CurrentX, selectedCehessman.CurrentY] = null;
            selectedCehessman.transform.position = GetTileCenter (x, y);
            selectedCehessman.SetPosition(x, y);
            Chessmans [x, y] = selectedCehessman;
            //Promjena reda
            isWhiteTurn = !isWhiteTurn;
        }

        selectedCehessman.GetComponent<MeshRenderer>().material = previousMat;

        //Sakrivanje plane objekta (sprjecavanje spawnanja vise kopija)
        BoardHighlights.Instance.HideHighlights();
        //Deselect ako kliknemo bilo gdje izvan moguceg odabira
        selectedCehessman = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        //Namjestanje detekcije na mrezi
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            seletionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            seletionX = -1;
            selectionY = -1;
        }
    }

    //Crtanje mreze
    private void DrawChessboard()
    {
        //Crte mreze po sirini
        Vector3 widthLine = Vector3.right * 8;//8 zbog broja polja
        //Crte mreze po visini (duzini)
        Vector3 heighhtLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            //Crtanje sirine
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= 8; j++)
            {
                //Crtanje duzine
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heighhtLine);
            }
        }

        //Draw the selection
        //Oznacavanje polja
        if(seletionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * seletionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (seletionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * seletionX,
                Vector3.forward * selectionY  + Vector3.right * (seletionX + 1));

        }
    }

    //Spawn figura
    private void SpawnChessman(int index, int x, int y, Quaternion rot)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), rot) as GameObject;
        go.transform.SetParent(transform);
        //Punjenje niza figurama
        Chessmans [x, y] = go.GetComponent<Chessman>();
        Chessmans [x, y].SetPosition(x, y);
        //Dodavanje u listu
        activeChessman.Add(go);
    }

    //Spawn svih figura
    private void SpawnAllChessman()
    {
        //Incijaliziranje liste
        activeChessman = new List<GameObject>();
        //Incijaliziranje niza
        Chessmans = new Chessman[8, 8];

        EnPassantMove = new int[2] { -1,-1};

        //Spawnanje bjelih

        //kralj
        SpawnChessman(0, 4, 0,whiteRot);

        //Kraljica
        SpawnChessman(1, 3, 0, whiteRot);

        //Top
        SpawnChessman(2, 0, 0, whiteRot);
        SpawnChessman(2, 7, 0, whiteRot);

        //Lovac
        SpawnChessman(3, 2, 0, whiteRot);
        SpawnChessman(3, 5, 0, whiteRot);

        //Konj
        SpawnChessman(4, 1, 0, whiteRot);
        SpawnChessman(4, 6, 0, whiteRot);

        //Pijuni
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i, 1, whiteRot);
        }

        //Spawn crnih

        //kralj
        SpawnChessman(6, 4, 7, blackRot);

        //Kraljica
        SpawnChessman(7, 3, 7, blackRot);

        //Top
        SpawnChessman(8, 0, 7, blackRot);
        SpawnChessman(8, 7, 7, blackRot);

        //Lovac
        SpawnChessman(9, 2, 7, blackRot);
        SpawnChessman(9, 5, 7, blackRot);

        //Konj
        SpawnChessman(10, 1, 7, blackRot);
        SpawnChessman(10, 6, 7, blackRot);

        //Pijuni
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6, blackRot);
        }
    }

    //Odredivanje pozicije figura
    private Vector3 GetTileCenter(int x, int z)
    {

        Vector3 origin = Vector3.zero; //Pocetna pozicija ploce (0,0,0)
        //Centriranje u sredinu kvadrata
        origin.x += (TileSize * x) + TileOffset;
        origin.z += (TileSize * z) + TileOffset;
        return origin;
    }

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("Bijeli je pobjedio");
        else
            Debug.Log("Crni je pobjedio");

        foreach (GameObject go in activeChessman)
            Destroy(go);

        //Reset igre
        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        SpawnAllChessman();
    }
}
