//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public BoardLogic boardLogic;
    public CameraControl cameraControl;
    public Player[] players;

    public Piece pieceBeingCaptured;
    public Piece pieceCapturing;

    public bool hasFocus = true;
    public bool moveTaken = false;

    public int activePlayerIndex;
    private InputManager inputManager;
    private BasicAI ai;

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
        players = new Player[2];
        players[0] = new Player("White", true,  PlayerType.Local , 0);
        players[1] = new Player("Black", false, Account.instance.opponentType, 1);

        //add lists to players
        LoadManager.FillPrefabs(players[0],Account.instance.GetArmyList(0));
        LoadManager.FillPrefabs(players[1],Account.instance.GetArmyList(1));
        //end

        GameNoticationCenter.instance.HoverSquare.Add(boardLogic.MouseOver);
        //GameNoticationCenter.instance.ClickedSquare.Add(LeftMouseClick);
        GameNoticationCenter.instance.RemoveHover.Add(boardLogic.RemoveMouseOver);

        activePlayerIndex = 0;
        //postMoveAction = PostMoveAction.None;
        inputManager = GetComponent<InputManager>();
        ai = new BasicAI(boardLogic, players[1]);
    }

    #region GAME FLOW

    private void Update() {
        if (!hasFocus)
            return;
        if (moveTaken && !activePlayer.noticationCenter.runner.running && !inactivePlayer.noticationCenter.runner.running)
        {
            NextPlayer();
        }
               /* case PostMoveAction.ReSet:
                    if (activePlayer.faction.type == FactionType.Phalanx)
                        foreach (Piece p in activePlayer.pieces)
                            p.moveWithCapture = true;
                    NextPlayer();
                    break;*/
    }

    public void NextPlayer(){
        //print("NextPlayer ");
        activePlayer.noticationCenter.runner.SetEndAction(RotatePlayers);
        activePlayer.noticationCenter.TriggerEvent(EventToTrigger.EndOfTurn);
    }

    void RotatePlayers() {
        //print("RotatePlayers");
        activePlayer.noticationCenter.runner.SetEndAction( null );
        activePlayerIndex = activePlayerIndex == 0 ? 1 : 0;
        //redo map threats
        boardLogic.map.RemoveThreats();
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
            StartCoroutine(ai.DecideMove());
        }
        moveTaken = false;
    }

    public void CheckMove( Vector2Int gridPoint, Piece selectedPiece){
        //check if user clicked valid move space
        if (selectedPiece == null && boardLogic.GridPointValidMoveTarget(gridPoint)) {
            //check castle move
            if (boardLogic.movingPiece.type == PieceType.King && !boardLogic.movingPiece.hasMoved)
            {
                if (gridPoint.x == 2 || gridPoint.x == 6) { }
                // move rook
            }
            boardLogic.Move(boardLogic.movingPiece, gridPoint);
            boardLogic.DeselectPiece(boardLogic.movingPiece);
            moveTaken = true;
        }
        //check if user clicked movingPiece; if yes then deselect it and go back a step
        else if (selectedPiece == boardLogic.movingPiece) {
            boardLogic.DeselectPiece(selectedPiece);
        }
        //check if user clicked on another friendly piece instead
        else if (DoesPieceBelongToCurrentPlayer(selectedPiece))
        {
            boardLogic.DeselectPiece(boardLogic.movingPiece);
            boardLogic.SelectPiece(selectedPiece);
        }
        //check if user clicked to capture a valid target
        else if (!DoesPieceBelongToCurrentPlayer(selectedPiece))
        {
            CheckCapture(gridPoint,selectedPiece);
        }
    }

    void CheckCapture(Vector2Int gridPoint, Piece selectedPiece) {
        //print("Checking Capture move");
        if (boardLogic.GridPointValidMoveTarget(gridPoint))  {
            pieceBeingCaptured = boardLogic.PieceAtGrid(gridPoint);
            pieceCapturing = boardLogic.movingPiece;
            if (pieceBeingCaptured != selectedPiece)
                print("PieceBeingCaptured and selectedPiece don't match!!");
            CapturePiece();
            moveTaken = true;
            if (pieceCapturing.moveWithCapture)
                boardLogic.Move(pieceCapturing, gridPoint);

            boardLogic.DeselectPiece(boardLogic.movingPiece);
            //check faction reactions
            activePlayer.noticationCenter.TriggerEvent(EventToTrigger.EnemyCaptured);
            inactivePlayer.noticationCenter.TriggerEvent(EventToTrigger.FriendlyCaptured);
           // if (inactivePlayer.faction.type == FactionType.Priest && inactivePlayer.faction.hasCaptureReaction)
             //   (inactivePlayer.faction as Priest).ReactToCapture(boardLogic.movingPiece);
            //deselect movingPiece
        }
    }

    public void PlacePiece(GameObject piece, Vector2Int gridPoint){
        if (boardLogic.GridPointValidMoveTarget(gridPoint)){
            boardLogic.AddPiece(piece, ActivePlayerIndex, gridPoint.x, gridPoint.y);
        }
        boardLogic.DeselectPiece(boardLogic.movingPiece);
    }
    #endregion

    #region INPUT HANDLER METHODS
    public void LeftMouseClick()
    {
        if (moveTaken || !hasFocus) return;

        var gridPoint = InputManager.lastGridPoint;
        if (!hasFocus) return;
        Piece selectedPiece = boardLogic.map.SquareAt(gridPoint).piece;
        if (boardLogic.movingPiece != null)
        {
            CheckMove(gridPoint, selectedPiece);
        }
        else if (selectedPiece != null)
        {
            boardLogic.SelectPiece(selectedPiece);
        }
    }

    public void ChangePowerButtonText( string text ){
        inputManager.ChangeButtonText(text);
    }

    public void PowerButtonPressed() {
        print("gamemanager power button pressed");
        GameNoticationCenter.TriggerEvent(GameEventTrigger.PowerPressed);
    }

    public void PowerButtonSwitch(bool isOn)
    {
        inputManager.ButtonVisable(isOn);
    }
    #endregion

    #region Logic Passthrough
    public void CapturePiece(Piece pieceCaptured , Piece pieceCapturing)
    {
        this.pieceBeingCaptured = pieceCaptured;
        this.pieceCapturing = pieceCapturing;
        boardLogic.CapturePieceAt(pieceCaptured.square.personalCoord);
        activePlayer.noticationCenter.TriggerEvent(EventToTrigger.EnemyCaptured);
        inactivePlayer.noticationCenter.TriggerEvent(EventToTrigger.FriendlyCaptured);
    }

    void CapturePiece( )   {
        boardLogic.CapturePieceAt(pieceBeingCaptured.square.personalCoord);
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
        for (int i = 0; i < players.Length; i++)
        {
            foreach (Piece p in players[i].pieces)
            {
                if (p.startOfTurnAction != null)
                    players[i].noticationCenter.TurnStart.Add(p.startOfTurnAction);
                if (p.endOfTurnAction != null)
                    players[i].noticationCenter.TurnEnd.Add(p.endOfTurnAction);
                if (p.friendlyCaptured != null)
                    players[i].noticationCenter.FriendlyCaptured.Add(p.friendlyCaptured);
                if (p.enemyCaptured != null)
                    players[i].noticationCenter.EnemyCaptured.Add(p.enemyCaptured);
            }
        }
    }
    #endregion
}
