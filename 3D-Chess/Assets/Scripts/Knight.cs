using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        //Gore Ljevo
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);

        //Gore desno
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);

        //Desno gore
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);

        //Desno dolje
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);

        //Dolje lijevo
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);

        //Dolje desno
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);

        //Lijevo gore
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);

        //Lijevo dolje
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;
    }


    //Da ne bi ponavljali kod bespotrebno peavimo metodu koju pozivamo u glavnoj funkciji
    //sa drugacijim parametrima
    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;

        if (x >= 0 && x < 8 && y >= 0 && y < 8)//Provjera da je konj u granicama ploce
        {
            c = ChessBoardManager.Instance.Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            //Provjera je li figura nasa ili protivnikova
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}
