using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This piece can capture a piece that's 2 spaces forward if the space in front of it is empty

public class Pawn_Charging : Pawn
{
    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        Square forwardNeighor = square.neighbors[ForwardDirection > 0 ? 0 : 4];
        if (forwardNeighor != null)
        {
            Square forwarder = forwardNeighor.neighbors[ForwardDirection > 0 ? 0 : 4];
            if (forwarder != null)
            {
                ThreatInTheory.Add(forwarder.UniqueID);
                if (forwarder.piece != null && forwarder.piece.CanBeDestroyedBy(this))
                {
                    ThreatWithPieces.Add(forwarder.UniqueID);
                }
            }
        }
    }
}