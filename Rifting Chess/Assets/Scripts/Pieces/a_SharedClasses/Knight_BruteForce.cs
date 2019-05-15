using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_BruteForce : Knight
{
    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();
        if (!specialMovementUsed)
        {
            foreach (int index in RookIndexes)
            {
                Square check = square;
                for (int i = 0; i < 3; i++)
                {
                    Square next = check.neighbors[index];
                    check = next;
                    if (next == null)
                        break;
                    if (i == 0 || next.piece != null)
                    {
                        continue;
                    }
                    moveLocations.Add(next.personalCoord);
                } 
            }
        }
    }

    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        foreach (int index in RookIndexes)
        {
            Square check = square;
            for (int i = 0; i < 3; i++)
            {
                Square next = check.neighbors[index];
                if (next == null)
                    break;
                if (i == 0)
                    continue;

                ThreatInTheory.Add(next.personalCoord);
                if (next.piece != null && next.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(next.personalCoord);
                }

                check = next;
            }
        }
    }

    public override void MoveTo(Vector2Int space, bool voluntary = true)
    {
        if (voluntary && (space.x == square.personalCoord.x || space.y == square.personalCoord.y))
            specialMovementUsed = true;
        base.MoveTo(space, voluntary);
    }
}

