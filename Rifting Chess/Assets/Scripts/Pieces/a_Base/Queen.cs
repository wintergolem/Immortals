using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceAdanced
{
    public override void Awake(){
        base.Awake();
        value = 9;
        type = PieceType.Queen;
    }

    protected override void CalculateMoveLocations()
    {
        moveLocations.Clear();
        bool canJump = currentMoveJump < maxMovePieceJumpsInstance;

        for ( int index = 0; index < square.neighbors.Length;index++)
        {
            MoveAlongAxis(index, canJump);
        }
    }

    protected override void CalculateThreatLocations()
    {
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;

        for (int index = 0; index < square.neighbors.Length; index++)
        {
            ThreatAlongAxis(index, canJump);
        }
    }

}
