using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trades places with a friendly piece just before that piece is captured. This is captured

public class Knight_Chivalrous : Knight
{
    List<Piece> shieldGuarding = new List<Piece>();

    public override void MoveTo(int square_id, bool voluntary = true)
    {
        base.MoveTo(square_id, voluntary);
        //remove previous shieldguarding
        for( int i = 0; i < shieldGuarding.Count -1; i++)
        {
            if (shieldGuarding[i] == null)
            {
                shieldGuarding.RemoveAt(i);
                i--;
            }
            else if (shieldGuarding[i].captureInterference == ShieldGuard)
            {
                shieldGuarding[i].captureInterference = null;
                shieldGuarding.RemoveAt(i);
                i--;
            }
        }
        //add new shieldguarding
        foreach (Square nein in square.neighbors)
        {
            if (nein != null && nein.piece != null && nein.piece.playerIndex == playerIndex)
            {
                nein.piece.captureInterference = ShieldGuard;
                shieldGuarding.Add(nein.piece);
            }
        }
    }


}
