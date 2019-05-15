using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Feinting : Pawn
{
    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();

        Square backNein = square.neighbors[ForwardDirection < 0 ? 4 : 0];
        if (backNein != null && backNein.piece == null)
        {
            moveLocations.Add(backNein.personalCoord);
        }
    }

    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        Square backNein = square.neighbors[ForwardDirection < 0 ? 4 : 0];
        if (backNein != null)
        {
            ThreatInTheory.Add(backNein.personalCoord);
            if (backNein.piece != null && backNein.piece.playerIndex != playerIndex)
            {
                ThreatWithPieces.Add(backNein.personalCoord);
            }
        }
    }
}