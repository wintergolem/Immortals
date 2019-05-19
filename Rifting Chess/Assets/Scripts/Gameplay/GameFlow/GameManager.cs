//using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public BoardLogic boardLogic;
    public CameraControl cameraControl;
    public DeploymentRunner deploymentRunner;
    public List<Player> players;

    public Piece_Base pieceBeingCaptured;
    public Piece_Base pieceCapturing;

    public List<object> stuffTakingFocus = new List<object>();
    bool HasFocus
    { get
        {
            return stuffTakingFocus.Count > 0;
        }
    }
    public bool moveTaken = false;
    public bool deploymentDone = false;
    public bool endHasRun = false;

    public int activePlayerIndex;
    private InputManager inputManager;

    public int ActivePlayerIndex{
        get { return activePlayerIndex; }
    }
    public Player activePlayer {
        get { return players[activePlayerIndex]; }
    }
    public Player inactivePlayer {
        get { return activePlayerIndex == 0 ? players[1] : players[0]; }
    }

    void Awake(){
        instance = this;
    }

    void Start() {
        print("manager - start");
        players = new List<Player>();
        //TODO: create GameInfo class that holds important info for following
        players.Add(new Player("White", true,  PlayerType.Local , 0) );
        players.Add(new Player("Black", false, Account.instance.opponentType, 1));

        //add lists to players
        LoadManager.FillPrefabs(players[0],Account.instance.GetArmyList(0) ,true);
        LoadManager.FillPrefabs(players[1],Account.instance.GetArmyList(1), false);
        //end
        deploymentRunner.Startup();
        GameNoticationCenter.instance.HoverSquare.Add(boardLogic.MouseOver);
        GameNoticationCenter.instance.RemoveHover.Add(boardLogic.RemoveMouseOver);

        activePlayerIndex = 0;
        inputManager = GetComponent<InputManager>();
    }

    void OnDestroy()
    {
        GameNoticationCenter.Surge();
        foreach (Player p in players)
            p.noticationCenter.Purge();
    }

    #region GAME FLOW

    private void Update() {
        if (!HasFocus || !deploymentDone)
            return;
        if (moveTaken && !activePlayer.noticationCenter.runner.running && !inactivePlayer.noticationCenter.runner.running)
        {
            if (endHasRun)
            {
                RotatePlayers();
            }
            else
            {
                activePlayer.noticationCenter.runner.SetEndAction(NextPlayer);
                activePlayer.noticationCenter.TriggerEvent(EventToTrigger.EndOfTurn);
                endHasRun = true;
            }
        }
    }

    public void NextPlayer(){
        boardLogic.UnhighlightSquares();
    }

    void RotatePlayers() {
        activePlayer.noticationCenter.runner.SetEndAction( null );
        activePlayerIndex = activePlayerIndex == 0 ? 1 : 0;
        foreach (Piece piece in activePlayer.pieces)
        {
            boardLogic.map.Threaten(piece.GetThreatLocations(), activePlayer.playerNumber);
        }
        if (activePlayer.type == PlayerType.Local)
        {
            cameraControl.SwitchCameras(ActivePlayerIndex);
            activePlayer.noticationCenter.TriggerEvent(EventToTrigger.StartOfTurn);
        }
        else if (activePlayer.type == PlayerType.AI)
        {

        }
        moveTaken = false;
    }

    public void PlacePiece(GameObject piece, Vector2Int gridPoint , int playerIndex = 0)
    {
        if (boardLogic.GridPointValidMoveTarget(gridPoint))
        {
            boardLogic.AddPiece(piece, ActivePlayerIndex, gridPoint.x, gridPoint.y);
        }
        boardLogic.UnhighlightSquares();
    }
    #endregion

    #region INPUT HANDLER METHODS
    public void ChangePowerButtonText( string text ){
        inputManager.ChangeButtonText(text);
    }

    public void PowerButtonPressed() {
        //print("gamemanager power button pressed");
        activePlayer.noticationCenter.TriggerEvent(EventToTrigger.PowerPressed);
    }

    public void PowerButtonSwitch(bool isOn)
    {
        inputManager.ButtonVisable(isOn);
    }
    #endregion

    #region Access funtions
    public void AddPieceToPlayer(int index, Piece piece) {
        players[index].pieces.Add(piece);
    }
    public bool DoesPieceBelongToCurrentPlayer(Piece piece) {
        return activePlayer.pieces.Contains(piece);
    }
    #endregion

    #region setup

    public void PiecesAdded()
    {
        /*for (int i = 0; i < players.Count; i++)
        {
            foreach (Piece p in players[i].pieces)
            {
                SetupNewPiece(p, i);
            }
       }*/ 
        activePlayerIndex = 1;
        RotatePlayers();
    }

    public void SetupNewPiece(Piece piece, int playerIndex)
    {
        if (piece.startOfTurnAction != null)
            players[playerIndex].noticationCenter.TurnStart.Add(piece.startOfTurnAction);
        if (piece.endOfTurnAction != null)
            players[playerIndex].noticationCenter.TurnEnd.Add(piece.endOfTurnAction);
        if (piece.friendlyCaptured != null)
            players[playerIndex].noticationCenter.FriendlyCaptured.Add(piece.friendlyCaptured);
        if (piece.enemyCaptured != null)
            players[playerIndex].noticationCenter.EnemyCaptured.Add(piece.enemyCaptured);
        if (piece.powerButtonPressed != null)
            players[playerIndex].noticationCenter.PowerPressed.Add(piece.powerButtonPressed);

        GameNoticationCenter.instance.PieceMoved.Add(piece.ClearUp);

        GameNoticationCenter.instance.ClickedPiece.Add(piece.CheckForClicked);
    }
    #endregion
}
