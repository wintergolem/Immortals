using System.Collections.Generic;
using UnityEngine;

public class Rook : PieceAdanced
{
    public override void Awake()
    {
        base.Awake();
        value = 5;
        type = PieceType.Rook;
    }

    protected override void CalculateMoveLocations()
    {
        moveLocations.Clear();
        bool canJump = currentMoveJump < maxMovePieceJumpsInstance;
        foreach (int index in RookIndexes)
        {
            MoveAlongAxis(index, canJump);
        }
    }

    protected override void CalculateThreatLocations()
    {
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;

        foreach (int index in RookIndexes)
        {
            ThreatAlongAxis(index, canJump);
        }
    }
}
