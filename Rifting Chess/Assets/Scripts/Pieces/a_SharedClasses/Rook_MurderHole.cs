using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook_MurderHole : Rook
{
    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        foreach (int index in BishopIndexes)
        {
            Square next = square.neighbors[index];

            if (next != null)
            {
                ThreatInTheory.Add(next.personalCoord);
                if (next.piece != null && next.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(next.personalCoord);
                }
            }
        }
    }
}
