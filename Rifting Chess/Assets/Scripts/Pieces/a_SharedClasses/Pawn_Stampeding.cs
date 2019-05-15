using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Stampeding : Pawn
{
    bool moveAbilityReseted = false;

    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();
        if (specialMovementUsed)
        {
            Square forwardNeighor = square.neighbors[ForwardDirection > 0 ? 0 : 4];
            if (forwardNeighor != null)
            {
                Square forwarder = forwardNeighor.neighbors[ForwardDirection > 0 ? 0 : 4];
                if (forwarder != null && forwarder.piece == null)
                {
                    moveLocations.Add(forwarder.personalCoord);
                }
            }
        }
    }

    public override void MoveTo(Vector2Int space, bool voluntary = true)
    {
        if (Mathf.Abs(space.y - square.personalCoord.y) == 2)
        {
            if (!specialMovementUsed)
            {
                specialMovementUsed = true;
            }
            else if ( !moveAbilityReseted )
            {
                moveAbilityReseted = true;
                specialMovementUsed = false;
            }
        }
        base.MoveTo(space, voluntary);
    }
}
