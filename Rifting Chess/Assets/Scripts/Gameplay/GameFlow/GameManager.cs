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

    public GameObject lostDialog;
    public GameObject wonDialog;

    public List<object> stuffTakingFocus = new List<object>();
    bool HasFocus
    { get
        {
            return stuffTakingFocus.Count > 0;
        }
    }
    public bool moveTaken = false;
    public bool deploymentDone = false;
    public bool deploymentStarted = false;
    public bool turnIsOver = false;

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

    bool gameOver = false;


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
        //check for null lists, should be for debugging only!
        if( Account.instance.GetArmyList(0) == null )
        {
            LoadManager.LoadAllLists();
        }
        LoadManager.FillPrefabs(players[0],Account.instance.GetArmyList(0));
        LoadManager.FillPrefabs(players[1],Account.instance.GetArmyList(1));
        GameNoticationCenter.instance.HoverSquare.Add(boardLogic.MouseOver);
        GameNoticationCenter.instance.RemoveHover.Add(boardLogic.RemoveMouseOver);

        activePlayerIndex = 0;
        inputManager = GetComponent<InputManager>();
    }

    void OnDestroy()
    {
        GameNoticationCenter.Purge();
        foreach (Player p in players)
            p.noticationCenter.Purge();
    }

    #region GAME FLOW

    private void Update() {
        if( !deploymentStarted )
        {
            deploymentStarted = true;
            deploymentRunner.Startup();
        }
        if (!HasFocus || !deploymentDone)
            return;
        if (gameOver)
        {
            if (activePlayer.GetKing() == null)
            {
                activePlayer.endDialog = lostDialog;
                inactivePlayer.endDialog = wonDialog;
                DisplayResults();
            }
            else if (inactivePlayer.GetKing() == null)
            {
                inactivePlayer.endDialog = lostDialog;
                activePlayer.endDialog = wonDialog;
                DisplayResults();
            }
            else
            {
                gameOver = false;
            }
        }
        else if (moveTaken && !activePlayer.noticationCenter.runner.running && !inactivePlayer.noticationCenter.runner.running)
        {
            if (turnIsOver)
            {
                RotatePlayers();
            }
            else
            {
                activePlayer.noticationCenter.runner.SetEndAction(NextPlayer);
                activePlayer.noticationCenter.TriggerEvent(EventToTrigger.EndOfTurn);
                turnIsOver = true;
            }
        }
    }

    public void NextPlayer(){
        boardLogic.RemoveAllIndicators();
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
        turnIsOver = false;
    }

    public void PlacePiece(GameObject piece, int square_id , int playerIndex, bool checkValidPlacement = true)
    {
        if (boardLogic.SquareValidMoveTarget(square_id) || !checkValidPlacement)
        {
            boardLogic.AddPiece(piece, ActivePlayerIndex, square_id);
        }
        else
        {
            Debug.LogWarning("Unable to place (" + piece.name + ") at " + square_id.ToString() + " due not being a valid move");
        }
        boardLogic.RemoveAllIndicators();
    }

    public void EndGame()
    {
        gameOver = true;
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

    public void DisplayResults()
    {
        activePlayer.endDialog.SetActive(true);
        stuffTakingFocus.Add(this);
    }
    #endregion
}
