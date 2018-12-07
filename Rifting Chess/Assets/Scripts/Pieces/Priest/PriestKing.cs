using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestKing : King {

    public Piece enemyPieceCaptured;

	public PriestKing()
    {
        friendlyCaptured = DivineRetributionTrigger;
    }

    public void DivineRetributionTrigger()
    {
        enemyPieceCaptured = GameManager.instance.pieceCapturing;
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Add(DivineRetributionBegin);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void DivineRetributionBegin()
    {
        GameNoticationCenter.instance.PowerPressed.Add(DivineRetributionEnd);
        GameNoticationCenter.instance.RightClick.Add(CancelDivineRetribution);
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(DivineRetributionBegin);
        GameManager.instance.PowerButtonSwitch(true);
        GameManager.instance.ChangePowerButtonText("Kill " + enemyPieceCaptured.displayName);
    }

    public void DivineRetributionEnd()
    {
        GameManager.instance.CapturePiece(enemyPieceCaptured, this);
        GameManager.instance.moveTaken = true;

        GameNoticationCenter.instance.PowerPressed.Remove(DivineRetributionEnd);
        GameNoticationCenter.instance.RightClick.Remove(CancelDivineRetribution);
        GameManager.instance.PowerButtonSwitch(false);

        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
    }

    public void CancelDivineRetribution()
    {
        GameNoticationCenter.instance.PowerPressed.Remove(DivineRetributionEnd);
        GameManager.instance.PowerButtonSwitch(false);

        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
    }
}
