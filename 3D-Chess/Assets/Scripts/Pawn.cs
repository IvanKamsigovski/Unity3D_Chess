using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c, c2;
        int[] e = ChessBoardManager.Instance.EnPassantMove;

        //Bijeli 
        if (isWhite)
        {
            //Dijagonalno ljevo
            if (CurrentX != 0 && CurrentY != 7)//Jesmo li se pomjeri sa prve tocke i jesmo li dosli do kraja ploce
            {
                //EnPasant
                if (e[0] == CurrentX - 1 && e[1] == CurrentY + 1)
                    r[CurrentX - 1, CurrentY + 1] = true;

                //Mrdanje figure
                c = ChessBoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                //Provjera ima li neprijateljske figure
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }

            //Dijagonalno desno
            if (CurrentX != 7 && CurrentY != 7)//Jesmo li se pomjeri sa prve tocke i jesmo li dosli do kraja ploce
            {
                //EnPasant
                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                    r[CurrentX + 1, CurrentY + 1] = true;

                //Mrdanje figure
                c = ChessBoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                //Provjera ima li neprijateljske figure
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }
            //Sredina
            if (CurrentY != 7)
            {
                c = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                //Ako nema figure ispred nas mozemo se mrdati
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }
            //Prvi potez
            if(CurrentY == 1)
            {
                c = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY + 2] = true;
            }
        }
        //Crni
        else
        {
            //Dijagonalno ljevo
            if (CurrentX != 0 && CurrentY != 0)//Jesmo li se pomjeri sa prve tocke i jesmo li dosli do kraja ploce
            {
                //EnPasant
                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                    r[CurrentX - 1, CurrentY - 1] = true;

                //Mrdanje figure
                c = ChessBoardManager.Instance.Chessmans[CurrentX - 1, CurrentY -1];
                //Provjera ima li neprijateljske figure
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }

            //Dijagonalno desno
            if (CurrentX != 7 && CurrentY != 0)//Jesmo li se pomjeri sa prve tocke i jesmo li dosli do kraja ploce
            {
                //EnPasant
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                    r[CurrentX + 1, CurrentY - 1] = true;

                //Mrdanje figure
                c = ChessBoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                //Provjera ima li neprijateljske figure
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }
            //Sredina
            if (CurrentY != 0)
            {
                c = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                //Ako nema figure ispred nas mozemo se mrdati
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }
            //Prvi potez
            if (CurrentY == 6)
            {
                c = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY -1];
                c2 = ChessBoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }
        }

        return r;
    }
}
