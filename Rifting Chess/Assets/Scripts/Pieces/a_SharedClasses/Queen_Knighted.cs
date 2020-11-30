using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen_Knighted : Queen
{

    List<int> specialMovements = new List<int>();
    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();

        if (!specialMovementUsed)
        {
            specialMovements.Clear();
            MoveAlongL( specialMovements );
        }
    }

    public override void MoveTo(int square_id , bool voluntary = true)
    {
        if (voluntary && !specialMovementUsed && specialMovements.Contains(square_id))
        {
            specialMovementUsed = true;
            specialMovements.Clear();
        }

        base.MoveTo(square_id, voluntary);
    }
}

