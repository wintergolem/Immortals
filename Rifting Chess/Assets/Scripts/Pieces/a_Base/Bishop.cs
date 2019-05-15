using System.Collections.Generic;
using UnityEngine;

public class Bishop : PieceAdanced
{
    public override void Awake() {
        base.Awake();
        value = 3;
        type = PieceType.Bishop;
    }

    protected override void CalculateMoveLocations()
    {
        moveLocations.Clear();
        bool canJump = currentMoveJump < maxMovePieceJumpsInstance;

        foreach (int index in BishopIndexes) 
        {
            MoveAlongAxis(index, canJump);
        }

    }

    protected override void CalculateThreatLocations()
    {
        moveLocations.Clear();
        bool canJump = currentMoveJump < maxMovePieceJumpsInstance;
        foreach (int index in BishopIndexes)
        {
            ThreatAlongAxis(index, canJump);
        }
    }
}
