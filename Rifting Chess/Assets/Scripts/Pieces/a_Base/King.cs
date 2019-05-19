using System.Collections.Generic;
using UnityEngine;

public class King : PieceAdanced
{

    public override void Awake()
    {
        base.Awake();
        value = 1500;
        type = PieceType.King;
        maxSquaresCanMove = 1;
    }

    protected override void CalculateMoveLocations( )
    {
        moveLocations.Clear();
        bool canJump = currentMoveJump < maxMovePieceJumpsInstance;

        for( int index = 0; index < square.neighbors.Length - 1; index++)
        {
            MoveAlongAxis(index, canJump);
        }
    }

    protected override void CalculateThreatLocations()
    {
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;

        for (int index = 0; index < square.neighbors.Length - 1; index++)
        {
            ThreatAlongAxis(index, canJump);
        }

    }

}
