using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Stampeding : Pawn
{
    int numberOfTimesAbilityUsed = 0;
    int numberOfTimesCanUseAblity = 2;
    int square_idOfSpecialMove;

    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();
        if (numberOfTimesAbilityUsed < numberOfTimesCanUseAblity)
        {
            Square forwardNeighor = square.neighbors[ForwardDirection > 0 ? 0 : 4];
            if (forwardNeighor != null)
            {
                Square forwarder = forwardNeighor.neighbors[ForwardDirection > 0 ? 0 : 4];
                if (forwarder != null && forwarder.piece == null)
                {
                    moveLocations.Add(forwarder.UniqueID);
                    square_idOfSpecialMove = forwarder.UniqueID;
                }
            }
        }
    }

    public override void MoveTo(int square_id, bool voluntary = true)
    {
        if (square_id == square_idOfSpecialMove)
        {
            numberOfTimesAbilityUsed++;
        }
        base.MoveTo(square_id, voluntary);
    }
}
