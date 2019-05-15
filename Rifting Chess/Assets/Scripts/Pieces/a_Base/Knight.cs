using System.Collections.Generic;
using UnityEngine;

public class Knight : PieceAdanced
{
    public override void Awake(){
        base.Awake();
        value = 3;
        type = PieceType.Knight;
    }

    protected override void CalculateMoveLocations()
    {
        moveLocations.Clear();
        MoveAlongL();
    }

    protected override void CalculateThreatLocations()
    {
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();

        ThreatAlongL();

    }
}
