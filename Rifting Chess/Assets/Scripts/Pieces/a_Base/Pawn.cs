using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceAdanced
{
    public override void Awake()
    {
        base.Awake();
        value = 1;
        type = PieceType.Pawn;
    }

    protected override void CalculateMoveLocations()
    {
        moveLocations.Clear();
        var indexes = ForwardDirection > 0 ? new[] {7,0,1} : new[]{3,4,5};

        Square neig = square.neighbors[indexes[1]];
        if (neig != null && neig.Empty)
        {
            moveLocations.Add(neig.personalCoord);
            if ( !hasMoved )
            {
                neig = neig.neighbors[indexes[1]];
                if(neig != null && neig.Empty)
                    moveLocations.Add(neig.personalCoord);
            }
        }


     
    }

    protected override void CalculateThreatLocations()
    {
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;
        var indexes = ForwardDirection > 0 ? new[] { 7, 0, 1 } : new[] { 3, 4, 5 };

        Square[] neigh = { square.neighbors[indexes[0]], square.neighbors[indexes[2]] };
        foreach (Square nein in neigh)
        {
            if (nein != null)
            {
                ThreatInTheory.Add(nein.personalCoord);
                if (!nein.Empty && nein.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(nein.personalCoord);
                }
            }
        }
    }

}
