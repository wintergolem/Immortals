using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarKing_Exodus : King
{
    private bool invincible = false;
    
    public WarKing_Exodus()
    {
        enemyCaptured = CheckThisPieceCaptured;
        maxSquaresCanMove = 3;
    }

    private void CheckThisPieceCaptured()
    {
        if (GameManager.instance.pieceCapturing == this)
        {
            GameLog.AddText("WarKing Exodus is now invincible for one turn!");
            TriggerInvincible();
            GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Add(TurnOffInvincible);
        }
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void TriggerInvincible()
    {
        invincible = true;
    }

    public void TurnOffInvincible()
    {
        invincible = false;
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(TurnOffInvincible);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public override bool CanBeDestroyedBy(Piece piece)
    {
        return base.CanBeDestroyedBy(piece) && !invincible;
    }
}
