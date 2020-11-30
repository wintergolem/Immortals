using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_MurderHole : Rook
{
    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;

        foreach (int index in BishopIndexes)
        {
            ThreatAlongAxis(index, canJump);
            /*Square next = square.neighbors[index];

            if (next != null)
            {
                ThreatInTheory.Add(next.UniqueID);
                if (next.piece != null && next.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(next.UniqueID);
                }
            }*/
        }
    }
}
