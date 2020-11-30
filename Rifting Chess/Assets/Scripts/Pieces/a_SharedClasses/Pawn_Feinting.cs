using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This piece can move backwards (cannot capture backwards)

public class Pawn_Feinting : Pawn
{
    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();

        Square backNein = square.neighbors[ForwardDirection < 0 ? 4 : 0];
        if (backNein != null && backNein.piece == null)
        {
            moveLocations.Add(backNein.UniqueID);
        }
    }

    /*protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        Square backNein = square.neighbors[ForwardDirection < 0 ? 4 : 0];
        if (backNein != null)
        {
            ThreatInTheory.Add(backNein.UniqueID);
            if (backNein.piece != null && backNein.piece.playerIndex != playerIndex)
            {
                ThreatWithPieces.Add(backNein.UniqueID);
            }
        }
    }*/
}