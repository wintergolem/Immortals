using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlustPawn : Pawn
{
    protected override void CalculateMoveLocations()
    {
        hasMoved = false;
        base.CalculateMoveLocations();
    }
}
