using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arch_Necromancer : King {

    public int summonPieceIndex;

    public Arch_Necromancer(){
        enemyCaptured = SummonPawn;
    }

    public new void Awake()
    {
        base.Awake();
        PieceInfo info = PieceList.allPieces.Find((obj) => obj.displayName == "Zombie");
        summonPieceIndex = LoadManager.AddToAdditional(player, info);
    }

    public void SummonPawn() {
        GameLog.AddText("Reap the Dead Triggered");
        var manager = GameManager.instance;
        if (manager.boardLogic.MovesForPiece(this).Count != 0)//boardLogic.MovesForPiece(king).Count != 0)//check for viable spaces
        {
            manager.boardLogic.SelectPiece(this);
            GameNoticationCenter.instance.ClickedSquare.Add(SummonPawnFinish);
            GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        }
        else
        {
            GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
            GameLog.AddText("No valid squares available, Reap the Dead cancelled");
        }
    }

    public void SummonPawnFinish(){
        var gridpoint = InputManager.lastSquareTouched;
        GameManager.instance.PlacePiece(LoadManager.additionalPieces[playerIndex][summonPieceIndex],gridpoint, playerIndex, false);
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonPawnFinish);
        GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
        GameLog.AddText("Reap the Dead Done");
    }

    public void CancelSummon(){
        GameLog.AddText("Reap the Dead cancelled");
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonPawnFinish);
        GameManager.instance.boardLogic.DeselectPiece(this);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }
}
