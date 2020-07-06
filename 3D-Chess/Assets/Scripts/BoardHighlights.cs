using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
   public static BoardHighlights Instance{ set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        //Pretraga liste za aktivnim objektom
        GameObject go = highlights.Find(g => !g.activeInHierarchy);

        //Ako nista ne nademo dodajemo novi objekt u listu
        if (go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }

        return go;
    }


    //Oznacavanje dozvoljenih poteza
    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject ();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void HideHighlights()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
}
