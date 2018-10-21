using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Faction {

    public Piece enemyToStrike;

    public Priest(){
        type = FactionType.Priest;
        hasCaptureReaction = true;
        reactionType = CaptureReactionType.Retribution;
        preMoveActionType = PreMoveActionType.Retribution;
        hasPowerToAttachToButton = false;
    }

    public void ReactToCapture(Piece aggressor){
        enemyToStrike = aggressor;
        hasPreMoveAction = true;
        hasPowerToAttachToButton = true;
    }

    public override void EndOfTurn(){
        hasPowerToAttachToButton = false;
        hasPreMoveAction = false;
        if ( enemyToStrike != null )
            GameManager.instance.boardLogic.HighlightPiece(enemyToStrike, true);
        enemyToStrike = null;
    }
}
