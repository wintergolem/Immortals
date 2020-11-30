using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This piece may move two spaces forward each turn instead of one.

public class WarlustPawn : Pawn
{
    protected override void CalculateMoveLocations()
    {
        hasMoved = false;
        base.CalculateMoveLocations();
    }
}
