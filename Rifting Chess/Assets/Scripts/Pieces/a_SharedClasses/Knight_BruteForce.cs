using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Once per game, you can ignore the change in direction. If activating this effect, you can also move one less square.
public class Knight_BruteForce : Knight
{
    List<int> square_idsForSpecial = new List<int>();

    protected override void CalculateMoveLocations()
    {
        base.CalculateMoveLocations();
        square_idsForSpecial.Clear();
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
                    moveLocations.Add(next.UniqueID);
                    square_idsForSpecial.Add(next.UniqueID);
                } 
            }
        }
    }

    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();
        bool canJump = currentCaptureJump < maxCapturePieceJumpsInstance;

        foreach (int index in RookIndexes)
        {
            ThreatAlongAxis(index, canJump);
            /*Square check = square;
            for (int i = 0; i < 3; i++)
            {
                Square next = check.neighbors[index];
                if (next == null)
                    break;
                if (i == 0)
                    continue;

                ThreatInTheory.Add(next.UniqueID);
                if (next.piece != null && next.piece.playerIndex != playerIndex)
                {
                    ThreatWithPieces.Add(next.UniqueID);
                }

                check = next;
            }*/
        }
    }

    public override void MoveTo(int space, bool voluntary = true)
    {
        if (voluntary && square_idsForSpecial.Contains(space))
            specialMovementUsed = true;
        base.MoveTo(space, voluntary);
    }
}

