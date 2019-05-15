using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Knighted : Queen
{


    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();

        if (!specialMovementUsed)
        {
            MoveAlongL();
        }
    }

    public override void MoveTo(Vector2Int space , bool voluntary = true)
    {
        if (voluntary && (space.x != square.personalCoord.x && space.y != square.personalCoord.y))
            specialMovementUsed = true;

        base.MoveTo(space, voluntary);
    }
}

