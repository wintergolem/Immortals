using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                ThreatInTheory.Add(forwarder.personalCoord);
                if (forwarder.piece != null && forwarder.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(forwarder.personalCoord);
                }
            }
        }
    }
}