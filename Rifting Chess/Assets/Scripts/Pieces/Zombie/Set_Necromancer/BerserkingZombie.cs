using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkingZombie : ZombiePawn {

    bool inFrenzy = false;
    bool waitingForNextTurn = false;

    public BerserkingZombie()
    {
        enemyCaptured = CheckIfThisCapturedPiece;
    }

    public void CheckIfThisCapturedPiece()
    {
        if (GameManager.instance.pieceCapturing == this)
        {
            if (!inFrenzy)
            {
                GameLog.AddText(displayName + "' entered a Frenzy");
                player.noticationCenter.TurnEnd.Add(EndFrenzy);
                inFrenzy = true;
            }
            waitingForNextTurn = true;
        }
        player.noticationCenter.runner.RunNext();
    }

    public void EndFrenzy()
    {
        if (!waitingForNextTurn)
        {
            GameLog.AddText(displayName + "'s Frenzy ended");
            player.noticationCenter.TurnEnd.Remove(EndFrenzy);
            inFrenzy = false;
        }
        else
        {
            waitingForNextTurn = false;
        }
       player.noticationCenter.runner.RunNext();

    }

    protected override void CalculateMoveLocations()
    {
        if (!inFrenzy)
             base.CalculateMoveLocations();
        else
        {
            moveLocations.Clear();
            foreach (Square neigh in square.neighbors)
            {
                if (!neigh.IsEmpty && neigh.piece.CanBeDestroyedBy(this))
                {
                    moveLocations.Add(neigh.UniqueID);
                }
            }
        }
    }
}
