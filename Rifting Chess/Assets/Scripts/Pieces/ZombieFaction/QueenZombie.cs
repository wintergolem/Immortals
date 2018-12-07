using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenZombie : Queen {

    public GameObject pieceToSummon;

    public QueenZombie(){
        enemyCaptured = CheckQueenCaptured;
    }
 
    public void CheckQueenCaptured(){
        if (GameManager.instance.pieceCapturing == this) {
            GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Add(SummonCapturedPiece);
        } 
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void CancelSummon(){
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(SummonCapturedPieceFinish);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void SummonCapturedPiece(){
        var manager = GameManager.instance;
        King king = (King)manager.activePlayer.GetKing();
        if (manager.boardLogic.MovesForPiece(king).Count != 0 ){
            manager.boardLogic.SelectPiece(king);
            pieceToSummon = manager.boardLogic.GetPieceTypeForPlayer(manager.pieceBeingCaptured.type,manager.players[playerIndex]);
            GameNoticationCenter.instance.ClickedSquare.Add(SummonCapturedPieceFinish);
            GameNoticationCenter.instance.RightClick.Add(CancelSummon);
        }
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(SummonCapturedPiece);
    }

    public void SummonCapturedPieceFinish(){
        var gridPoint = InputManager.lastGridPoint;
        GameManager.instance.PlacePiece(pieceToSummon, gridPoint);
        GameManager.instance.moveTaken = true;
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonCapturedPieceFinish);
        GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }
}
