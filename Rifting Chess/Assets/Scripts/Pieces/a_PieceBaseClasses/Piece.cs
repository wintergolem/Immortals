using System.Collections.Generic;
using UnityEngine;
using System;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Piece : Piece_Base
{
    public Action startOfTurnAction;
    public Action endOfTurnAction;
    public Action friendlyCaptured;
    public Action enemyCaptured;
    public Action powerButtonPressed;

    public delegate void CaptureInterference(Piece piece);
    public CaptureInterference captureInterference;

    protected List<Vector2Int> highlightLocations = new List<Vector2Int>();

    protected Piece()
    {
    }

    public virtual void Awake()
    {
        //cheater stuff to avoid crashes on pieces that summon
        if (GameManager.instance.deploymentDone)
            player = GameManager.instance.players[playerIndex];
        else
            player = GameManager.instance.players[DeploymentRunner.instance.activePlayerIndex];
    }

    public virtual void CheckMap() {
        return;
    }

    #region movement and threat
    protected int maxSquaresCanMove = 8;
    protected int[] RookIndexes = { 0, 2, 4, 6 };
    protected int[] BishopIndexes = { 1, 3, 5, 7 };

    protected int maxMovePieceJumpsCon = 0;
    protected int maxMovePieceJumpsInstance = 0;
    protected int currentMoveJump = 0;

    protected int maxCapturePieceJumpsCon = 0;
    protected int currentCaptureJump = 0;
    protected int maxCapturePieceJumpsInstance = 0;

    protected abstract void CalculateMoveLocations(); //shows which squares can be moved to
    protected List<Vector2Int> moveLocations = new List<Vector2Int>();
    public List<Vector2Int> MoveLocations
    {
        get
        {
            if (moveLocations == null || moveLocations.Count <= 0)
            {
                CalculateMoveLocations();
            }
            return moveLocations;
        }
    }
    protected List<Vector2Int> ThreatWithPieces = new List<Vector2Int>();
    protected List<Vector2Int> ThreatInTheory = new List<Vector2Int>();
    protected  abstract void CalculateThreatLocations(); //shows which squares can be attacked
    public List<Vector2Int> GetThreatLocations(bool asBoardIs = false)
    {
        if (ThreatInTheory.Count <= 0)
        {
            CalculateThreatLocations();
        }

        if (asBoardIs)
        {
            return ThreatWithPieces;
        }
        else
        {
            return ThreatInTheory;
        }
    }

    #endregion

    public virtual void OnCaptured()
    {
        if (captureInterference != null)
        {
            captureInterference(this);
        }
        else
        {
            player.noticationCenter.TriggerEvent(EventToTrigger.FriendlyCaptured);
            player.pieces.Remove(this);
            square.RemovePiece();
            square = null;

            if (selected)
                UnSelect();
            GameNoticationCenter.instance.ClickedPiece.Remove(CheckForClicked);
            GameNoticationCenter.instance.ClickedSquare.Remove(WaitForClickOnSquare);
            GameNoticationCenter.instance.ClickedPiece.Remove(WaitForClickOnPiece);
            GameNoticationCenter.instance.PieceMoved.Remove(ClearUp);
            GameNoticationCenter.instance.runner.RemovePiece(this);

            if (startOfTurnAction != null)
                player.noticationCenter.TurnStart.Remove(startOfTurnAction);
            if (endOfTurnAction != null)
                player.noticationCenter.TurnEnd.Remove(endOfTurnAction);
            if (friendlyCaptured != null)
                player.noticationCenter.FriendlyCaptured.Remove(friendlyCaptured);
            if (enemyCaptured != null)
                player.noticationCenter.EnemyCaptured.Remove(enemyCaptured);
            if (powerButtonPressed != null)
                player.noticationCenter.PowerPressed.Remove(powerButtonPressed);

            Destroy(gameObject);
        }
    }

    bool mapThreatened = false;
    public void CheckForClicked()
    {
        if (InputManager.lastGridPoint == square.personalCoord) //clicked on this piece
        {
            if (GameManager.instance.activePlayerIndex != playerIndex) //opponent clicked on this piece
            {
                if (mapThreatened)
                {
                    GameManager.instance.boardLogic.UnhighlightSquares();
                    highlightLocations.Clear();
                    mapThreatened = false;
                }
                else
                {
                    HighlightThreat();
                    mapThreatened = true;
                }
            }
            else if (selected)//piece was already selected, remove selection
            {
                UnSelect();
            }
            else  //user wants this piece selected 
            {
                Select();
            }
        }
        else //this piece was not clicked on, but a click was made
        {
            if (GameManager.instance.activePlayerIndex != playerIndex) //opponent clicked elsewhere
            {
                if (mapThreatened)
                {
                    GameManager.instance.boardLogic.UnhighlightSquares();
                    mapThreatened = false;
                }
            }
            if (GameManager.instance.boardLogic.map.SquareAt(InputManager.lastGridPoint).piece != null && selected) //another piece was clicked on
            {
                if (GameManager.instance.boardLogic.map.SquareAt(InputManager.lastGridPoint).piece.playerIndex == playerIndex) //other piece was friendly, unselect this piece
                {
                    UnSelect();
                }
            }
        }

        GameNoticationCenter.instance.runner.RunNext();
    }

    public void WaitForClickOnSquare()
    {
        var target = InputManager.lastGridPoint;
        if (MoveLocations.Contains(target)) //square is a valid square to move to
        {
            MoveTo(target);
            GameManager.instance.moveTaken = true;
        }
        UnSelect();
        GameNoticationCenter.instance.runner.RunNext();
    }

    public void WaitForClickOnPiece()
    {
        var target = InputManager.lastGridPoint;
        if (target == square.personalCoord)
        {
            UnSelect();
        }
        else if (ThreatWithPieces.Contains(target)) //square is a valid square to move to
        {
            var targetPiece = GameManager.instance.boardLogic.map.SquareAt(target).piece;
            if (targetPiece.playerIndex == playerIndex) //player clicked on another friendly piece, unselect this piece
            {
                UnSelect();
            }
            else //attack piece at square
            {
                CaptureEnemyPiece(targetPiece, target);
            }
        }
        else //user clicked outside this piece's realm of action
        {
            UnSelect();
        }
        GameNoticationCenter.instance.runner.RunNext();
    }

    public void HighlightThreat( bool boardAsIs = false)
    {
        List<Vector2Int> locations = MoveLocations;
        GameManager.instance.boardLogic.HighlightSquares(locations, true);
        highlightLocations.Clear();
        highlightLocations.AddRange(locations);
        locations.Clear();
        locations = GetThreatLocations(boardAsIs);
        GameManager.instance.boardLogic.HighlightSquares(locations, false);
        highlightLocations.AddRange(locations);
    }

    public void Highlight()
    {
        GameManager.instance.boardLogic.HighlightPiece(this);
        HighlightThreat(true);
    }

    public void RemoveHighlight()
    {
        GameManager.instance.boardLogic.HighlightPiece(this, true);
        GameManager.instance.boardLogic.UnhighlightSquares(highlightLocations);
        mapThreatened = false;
    }

    protected void Select()
    {
        selected = true;
        Highlight();
        GameNoticationCenter.instance.ClickedPiece.Insert(0, WaitForClickOnPiece);
        GameNoticationCenter.instance.ClickedSquare.Insert(0, WaitForClickOnSquare);
        if (!GameNoticationCenter.instance.ClickedPiece.Remove(CheckForClicked))
            print("No CheckForClicked found");
    }

    protected override void UnSelect()
    {
        selected = false;
        RemoveHighlight();
        GameNoticationCenter.instance.ClickedSquare.Remove(WaitForClickOnSquare);
        GameNoticationCenter.instance.ClickedPiece.Remove(WaitForClickOnPiece);
        GameNoticationCenter.instance.ClickedPiece.Add(CheckForClicked);
    }

    public virtual void ClearUp()
    {
        moveLocations.Clear();
        ThreatInTheory.Clear();
        ThreatWithPieces.Clear();
        GameNoticationCenter.instance.runner.RunNext();
    }
}
