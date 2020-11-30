using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestKing : King {

    public Piece_Base enemyPieceCaptured;

	public PriestKing()
    {
        friendlyCaptured = DivineRetributionTrigger;
    }

    public void DivineRetributionTrigger()
    {
        GameLog.AddText("Divine Retribution triggered");
        enemyPieceCaptured = GameManager.instance.pieceCapturing;
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Add(DivineRetributionBegin);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void DivineRetributionBegin()
    {
        GameLog.AddText("Divine Retribution active");
        player.noticationCenter.PowerPressed.Add(DivineRetributionEnd);
       GameNoticationCenter.instance.RightClick.Add(CancelDivineRetribution);
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(DivineRetributionBegin);
        GameManager.instance.PowerButtonSwitch(true);
        GameManager.instance.ChangePowerButtonText("Kill " + enemyPieceCaptured.displayName);
    }

    public void DivineRetributionEnd()
    {
        GameLog.AddText( enemyPieceCaptured.displayName + " captured via Divine Retribution" );
        CaptureEnemyPiece(enemyPieceCaptured, -1); 
        GameManager.instance.moveTaken = true;

        player.noticationCenter.PowerPressed.Remove(DivineRetributionEnd);
        GameNoticationCenter.instance.RightClick.Remove(CancelDivineRetribution);
        GameManager.instance.PowerButtonSwitch(false);

        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
        player.noticationCenter.runner.RunNext();
    }

    public void CancelDivineRetribution()
    {
        GameLog.AddText(" Divine Retribution Cancelled");
        player.noticationCenter.PowerPressed.Remove(DivineRetributionEnd);
        GameManager.instance.PowerButtonSwitch(false);

        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
        player.noticationCenter.runner.RunNext();
    }
}
