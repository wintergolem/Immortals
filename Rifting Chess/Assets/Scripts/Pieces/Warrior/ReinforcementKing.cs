﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforcementKing : King {

    public int numberOfTimesPowerCalled = 3;
    public PieceType typeToSummon = PieceType.Knight;
    public List<int> locations;

    int summonPieceIndex;

    public ReinforcementKing()
    {
        powerButtonPressed = CallForReinforcements;
        startOfTurnAction = TurnOnButton;
        endOfTurnAction = TurnOffButton;
    }

    public override void Awake()
    {
        base.Awake();
        PieceInfo info = PieceList.allPieces.Find((obj) => obj.displayName == "wKnight");
        summonPieceIndex = LoadManager.AddToAdditional(player, info);
    }

    public void TurnOnButton()
    {
        GameManager.instance.PowerButtonSwitch(true);
        GameManager.instance.ChangePowerButtonText("Call for Reinforcements");
        player.noticationCenter.runner.RunNext();
    }

    public void TurnOffButton()
    {
        GameManager.instance.PowerButtonSwitch(false);
        player.noticationCenter.runner.RunNext();
    }

    public void CallForReinforcements()
    {
        if (GameManager.instance.moveTaken)
        {
            player.noticationCenter.runner.RunNext();
            return;
        }
        numberOfTimesPowerCalled  -= 1;
        if (numberOfTimesPowerCalled == 0)
        {
            GameLog.AddText(displayName + "''s Final Reinforcements are on their way!");
            player.noticationCenter.PowerPressed.Remove(CallForReinforcements);
            player.noticationCenter.TurnStart.Remove(TurnOnButton);
            player.noticationCenter.TurnEnd.Remove(TurnOffButton);
            TurnOffButton();
        }
        else
        {
            GameLog.AddText("'Reinforcements are on their way!");
        }
        GameManager.instance.moveTaken = true;
        player.noticationCenter.TurnStart.Add(ShowOptions);
        player.noticationCenter.runner.RunNext();
    }

    public void ShowOptions()
    {

        player.noticationCenter.TurnStart.Remove(ShowOptions);
        TurnOffButton();
        locations = GameManager.instance.boardLogic.GetEmptySpawnPointsForPlayer(playerIndex);
        if (locations.Count == 0)
        {
            CancelReinforcement();
            player.noticationCenter.runner.RunNext();
            return;
        }
        GameLog.AddText("Select Square for Reinforcement piece!");
        GameManager.instance.boardLogic.SquaresWithIndicators(locations, true);//highlight squares
        GameNoticationCenter.instance.ClickedSquare.Add(SelectSquare);
    }

    public void SelectSquare()
    {
        var selection = InputManager.lastSquareTouched;
        if (locations.Contains(selection))
        {
            GameLog.AddText("Reinforcements Arrived!");
            GameManager.instance.boardLogic.RemoveAllIndicators(); //unhighlight squares
            GameManager.instance.PlacePiece(LoadManager.additionalPieces[playerIndex][summonPieceIndex], selection, this.playerIndex, false); //summon piece to selectedSquare ignoring placement validity

            GameNoticationCenter.instance.ClickedSquare.Remove(SelectSquare);
            GameNoticationCenter.instance.RightClick.Remove(CancelReinforcement);
            GameNoticationCenter.instance.runner.RunNext();

            player.noticationCenter.runner.RunNext();
        }
    }

    public void CancelReinforcement()
    {
        GameLog.AddText(displayName + "''s Call for Reinforcements Cancelled!");
        GameManager.instance.boardLogic.RemoveAllIndicators();//unhighlight squares
        if (numberOfTimesPowerCalled == 0)
        {
            player.noticationCenter.PowerPressed.Add(CallForReinforcements);
            player.noticationCenter.TurnStart.Add(TurnOnButton);
            player.noticationCenter.TurnEnd.Add(TurnOffButton);
        }
        numberOfTimesPowerCalled += 1;

        GameNoticationCenter.instance.ClickedSquare.Remove(SelectSquare);
        GameNoticationCenter.instance.RightClick.Remove(CancelReinforcement);
        GameNoticationCenter.instance.runner.RunNext();

        player.noticationCenter.runner.RunNext();
    }
}
