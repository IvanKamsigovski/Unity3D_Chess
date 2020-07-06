using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        //Gore lijevo
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            //Provjera jesmo li u granicama
            if (i < 0 || j >= 8)
                break;

            c = ChessBoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;//Jer nam nasa figura blokira prolaz
            }
        }

        //Gore desno
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            //Provjera jesmo li u granicama
            if (i >= 8 || j >= 8)
                break;

            c = ChessBoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;//Jer nam nasa figura blokira prolaz
            }
        }

        //Dolje lijevo
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            //Provjera jesmo li u granicama
            if (i < 0 || j < 0)
                break;

            c = ChessBoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;//Jer nam nasa figura blokira prolaz
            }
        }

        //Dolje desno
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            //Provjera jesmo li u granicama
            if (i >= 8 || j < 0)
                break;

            c = ChessBoardManager.Instance.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;//Jer nam nasa figura blokira prolaz
            }
        }

        return r;
    }
}
