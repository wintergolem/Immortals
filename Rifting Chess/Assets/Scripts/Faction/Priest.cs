using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Faction {

    public Piece enemyToStrike;

    public Priest(){
        type = FactionType.Priest;
    }

    public void ReactToCapture(Piece aggressor){
        enemyToStrike = aggressor;
    }

    public  void EndOfTurn(){
        if ( enemyToStrike != null )
            GameManager.instance.boardLogic.HighlightPiece(enemyToStrike, true);
        enemyToStrike = null;
    }
}
