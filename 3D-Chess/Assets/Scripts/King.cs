using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        //Gore
        i = CurrentX - 1;
        j = CurrentY + 1;
        if (CurrentY != 7)//Ako je 7 dosli smo do zadnje pozicije na ploci
        {
            for (int k = 0; k < 3; k++)//k < 3 je rprovjeravamo tri pozicije
            {
                if (i >= 0 || i < 8)
                {
                    c = ChessBoardManager.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //Dolje
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)//Ako je 0 dosli smo do zadnje pozicije na ploci
        {
            for (int k = 0; k < 3; k++)//k < 3 je rprovjeravamo tri pozicije
            {
                if (i >= 0 || i < 8)
                {
                    c = ChessBoardManager.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //Sredina lijevo
        if (CurrentX != 0)
        {
            c = ChessBoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;

        }

        //Sredina desno
        if (CurrentX != 7)
        {
            c = ChessBoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null) 
                r[CurrentX + 1, CurrentY] = true; 
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;

        }

        return r;
    }
}
