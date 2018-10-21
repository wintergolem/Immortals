//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum GameState { premove, move, postmove, AI };
public enum PostMoveAction { None , Summon, Castle , ReSet };

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public BoardLogic boardLogic;
    public CameraControl cameraControl;
    public GameState state;
    public Player[] players;

    private bool hasMoved = false;
    private int activePlayerIndex;
    private InputManager inputManager;
    private PostMoveAction postMoveAction;
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
        if (Account.instance == null)
            Account.CreateAccount();
        players = new Player[2];
        players[0] = new Player("White", true, Account.instance.factionToBePlayed, PlayerType.Local , 0);
        players[1] = new Player("Black", false, Account.instance.factionOpponent, Account.instance.opponentType, 1);
        activePlayerIndex = 0;
        state = GameState.premove;
        postMoveAction = PostMoveAction.None;
        inputManager = GetComponent<InputManager>();
        inputManager.ButtonVisable(false);
        ai = new BasicAI(boardLogic, players[1]);
    }



    #region GAME FLOW

    private void Update()
    {
        if (state == GameState.postmove) {
            switch (postMoveAction) {
                case PostMoveAction.None:
                    NextPlayer();
                    break;
                case PostMoveAction.Castle:
                    Piece king = activePlayer.GetKing();
                    if (king.square.personalCoord.x == 2) {
                        //grab rook
                        Piece rook = boardLogic.PieceAtGrid(new Vector2Int(0, king.square.personalCoord.y));
                        boardLogic.Move(rook, new Vector2Int(3, king.square.personalCoord.y));
                    } else  {
                        Piece rook = boardLogic.PieceAtGrid(new Vector2Int(7, king.square.personalCoord.y));
                        boardLogic.Move(rook, new Vector2Int(5, king.square.personalCoord.y));
                    }
                    NextPlayer();
                    break;
                case PostMoveAction.ReSet:
                    if (activePlayer.faction.type == FactionType.Phalanx)
                        foreach (Piece p in activePlayer.pieces)
                            p.moveWithCapture = true;
                    NextPlayer();
                    break;
                case PostMoveAction.Summon:
                    if (activePlayer.type == PlayerType.AI) {
                        ai.ChoosePawnSummon();
                    }
                    break;
            }
        }
    }

    public void NextPlayer(){
        activePlayer.faction.EndOfTurn();
        activePlayerIndex = activePlayerIndex == 0 ? 1 : 0;

        //redo map threats
        boardLogic.map.RemoveThreats();
        foreach (Piece piece in inactivePlayer.pieces){
            boardLogic.map.Threaten(piece.GetThreatLocations() , inactivePlayer.playerNumber);
        }


        if (activePlayer.type == PlayerType.Local){
            cameraControl.SwitchCameras(ActivePlayerIndex);
            state = GameState.premove;
            PreformPreMoveAction();
            inputManager.ButtonVisable(false);
        } else if (activePlayer.type == PlayerType.AI){
            inputManager.ButtonVisable(false);
            state = GameState.AI;
            StartCoroutine(ai.DecideMove());
            //TODO do what?
        }
        postMoveAction = PostMoveAction.None;
        hasMoved = false;
    }

    void PreformPreMoveAction()
    {
        if (activePlayer.faction.hasPreMoveAction)
        {
            switch (activePlayer.faction.preMoveActionType)
            {
                case PreMoveActionType.Summon:
                    postMoveAction = PostMoveAction.Summon;
                    boardLogic.SelectPiece(activePlayer.GetKing());
                    state = GameState.postmove;
                    //clear faction flags
                    activePlayer.faction.hasPreMoveAction = false;
                    activePlayer.faction.preMoveActionType = PreMoveActionType.None;
                    break;
                case PreMoveActionType.CheckMap:
                    foreach (Piece p in activePlayer.pieces) {
                        p.CheckMap();
                    }
                    break;
                case PreMoveActionType.Retribution:
                    boardLogic.HighlightPiece((activePlayer.faction as Priest).enemyToStrike);
                    inputManager.ButtonVisable(true);
                    ChangePowerButtonText( "Kill " + (activePlayer.faction as Priest).enemyToStrike.type.ToString());
                    break;
            }
        }
    }

    public void CheckMove( Vector2Int gridPoint, Piece selectedPiece){
        //check if user clicked valid move space
        if (selectedPiece == null && boardLogic.GridPointValidMoveTarget(gridPoint)) {
            //check castle move
            if (boardLogic.movingPiece.type == PieceType.King && !boardLogic.movingPiece.hasMoved)
            {
                if (gridPoint.x == 2 || gridPoint.x == 6)
                    postMoveAction = PostMoveAction.Castle;
            }
            boardLogic.Move(boardLogic.movingPiece, gridPoint);
            boardLogic.DeselectPiece(boardLogic.movingPiece);
            hasMoved = true;
            state = GameState.postmove;
        }
        //check if user clicked movingPiece; if yes then deselect it and go back a step
        else if (selectedPiece == boardLogic.movingPiece)
        {
            state = GameState.premove;
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
        if (boardLogic.GridPointValidMoveTarget(gridPoint))  {
            PieceType capturedPieceType = boardLogic.PieceAtGrid(gridPoint).type;
            PieceType movingPieceType = boardLogic.movingPiece.type;
            CapturePiece(selectedPiece);//boardLogic.CapturePieceAt(gridPoint);
            hasMoved = true;
            if (boardLogic.movingPiece.moveWithCapture)
                boardLogic.Move(boardLogic.movingPiece, gridPoint);

            state = GameState.postmove;
            //check faction reactions
            if (inactivePlayer.faction.type == FactionType.Priest && inactivePlayer.faction.hasCaptureReaction)
                (inactivePlayer.faction as Priest).ReactToCapture(boardLogic.movingPiece);
            //deselect movingPiece
            boardLogic.DeselectPiece(boardLogic.movingPiece);
            if (activePlayer.faction.type == FactionType.Zombies) 
                postMoveAction = (activePlayer.faction as Zombies).CheckSummon(boardLogic,activePlayer,movingPieceType,capturedPieceType);
        }
    }

    public void PlacePawn(Vector2Int gridPoint){
        if (boardLogic.GridPointValidMoveTarget(gridPoint)){
            boardLogic.AddPiece((activePlayer.faction as Zombies).pieceToSummonPath, ActivePlayerIndex, gridPoint.x, gridPoint.y);
        }
        boardLogic.DeselectPiece(boardLogic.movingPiece);
        postMoveAction = PostMoveAction.None;
        hasMoved = true;
    }
    #endregion

    #region INPUT HANDLER METHODS
    public void MouseOver(Vector2Int gridPoint) {
        boardLogic.MouseOver(gridPoint);
    }
    public void RemoveMouseOver(){
        boardLogic.RemoveMouseOver();
    }
    public void LeftMouseClick(Vector3 point)
    {
        Vector2Int gridPoint = Geometry.GridFromPoint(point);
        Piece selectedPiece = boardLogic.map.SquareAt(gridPoint).piece;

        switch (state)
        {
            case GameState.premove:
                if (selectedPiece != null && DoesPieceBelongToCurrentPlayer(selectedPiece))
                {
                    boardLogic.SelectPiece(selectedPiece);
                    state = GameState.move;
                }
                break;
            case GameState.move:
                CheckMove(gridPoint, selectedPiece);
                break;
            case GameState.postmove:
                switch (postMoveAction)
                {
                    case PostMoveAction.Summon:
                        if (selectedPiece == null)
                            PlacePawn(gridPoint);
                        break;
                    case PostMoveAction.Castle: //shouldn't ever come up
                        break;
                }
                break;
        }
    }
    public void RightMouseClick(Vector3 point)
    {
        switch (state)
        {
            case GameState.postmove:
                if (postMoveAction == PostMoveAction.Summon) {
                    postMoveAction = PostMoveAction.None;
                    boardLogic.DeselectPiece(boardLogic.movingPiece);
                    if (!hasMoved)
                        state = GameState.premove;
                }
                break;
            case GameState.move:
                if (activePlayer.faction.preMoveActionType == PreMoveActionType.Retribution) {
                    //nothing
                }
                break;
            default:
                return;
        }
    }

    void ChangePowerButtonText( string text ){
        inputManager.ChangeButtonText(text);
    }

    public void PowerButtonPressed()
    {
        if (activePlayer.faction.type == FactionType.Phalanx && activePlayer.faction.hasPowerToAttachToButton){
            activePlayer.faction.PowerAttachedToButton();
            postMoveAction = PostMoveAction.ReSet;
            foreach (Piece p in activePlayer.pieces){
                p.moveWithCapture = false;
            }
        } else if (activePlayer.faction.type == FactionType.Priest) {
            CapturePiece((activePlayer.faction as Priest).enemyToStrike);
            activePlayer.faction.hasPreMoveAction = false;
            activePlayer.faction.hasCaptureReaction = false;
            state = GameState.postmove;
        }
    }
    #endregion

    #region Logic Passthrough
    void CapturePiece(Piece piece)   {
        boardLogic.CapturePieceAt(piece.square.personalCoord);
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
}
