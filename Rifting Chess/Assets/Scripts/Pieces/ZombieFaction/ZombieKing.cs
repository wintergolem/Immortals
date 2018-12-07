using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieKing : King {

    public GameObject pieceToSummon;

    public ZombieKing(){
        enemyCaptured = SummonPawn;
    }

    public void SummonPawn() {
        //print("SummonPawn called");
        var manager = GameManager.instance;
        if ( manager.boardLogic.MovesForPiece(this).Count != 0 )//boardLogic.MovesForPiece(king).Count != 0)//check for viable spaces
        {
            pieceToSummon = manager.boardLogic.GetPieceTypeForPlayer(PieceType.Pawn, manager.activePlayer);
            manager.boardLogic.SelectPiece(this);
            GameNoticationCenter.instance.ClickedSquare.Add(SummonPawnFinish);
            GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        } else
            GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void SummonPawnFinish(){
        //print("SummonPawnFinish called");
        var gridpoint = InputManager.lastGridPoint;
        GameManager.instance.PlacePiece(pieceToSummon,gridpoint);
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonPawnFinish);
        GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
    }

    public void CancelSummon(){
        //print("SummonPawn  Cancel called");
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonPawnFinish);
        GameManager.instance.boardLogic.DeselectPiece(this);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }
}
