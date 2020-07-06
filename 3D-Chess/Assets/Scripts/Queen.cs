using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        //KOPIJA TOPA
        //Desno
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)//Ako smo presli preko kraja ploce
                break;

            //Ako smo ostali u granicama
            c = ChessBoardManager.Instance.Chessmans[i, CurrentY];//Y jer idemo desno
            //Ako nema figure u nasem smjeru
            if (c == null)
                r[i, CurrentY] = true;
            //Ako ima figura na putu
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //Ljevo
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)//Ako smo presli preko kraja ploce
                break;

            //Ako smo ostali u granicama
            c = ChessBoardManager.Instance.Chessmans[i, CurrentY];//Y jer idemo desno
            //Ako nema figure u nasem smjeru
            if (c == null)
                r[i, CurrentY] = true;
            //Ako ima figura na putu
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //Gore
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)//Ako smo presli preko kraja ploce
                break;

            //Ako smo ostali u granicama
            c = ChessBoardManager.Instance.Chessmans[CurrentX, i];//Y jer idemo desno
            //Ako nema figure u nasem smjeru
            if (c == null)
                r[CurrentX, i] = true;
            //Ako ima figura na putu
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        //Dole
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)//Ako smo presli preko kraja ploce
                break;

            //Ako smo ostali u granicama
            c = ChessBoardManager.Instance.Chessmans[CurrentX, i];//Y jer idemo desno
            //Ako nema figure u nasem smjeru
            if (c == null)
                r[CurrentX, i] = true;
            //Ako ima figura na putu
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        //-----------------------------------------------------------------------------------
        //KOPIJA LOVCA
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
