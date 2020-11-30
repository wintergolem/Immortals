using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece_Base : MonoBehaviour
{
    public bool hasMoved = false;
    public bool selected = false;

    public bool moveWithCapture = true;
    public int numberOfPiecesJump = 0;
    public int value = 0;
    public string displayName;

    public PieceType type;
    public Square square;
    public int playerIndex;
    protected Player player;

    public void AssignPlayer(int playerIndex)
    {
        this.playerIndex = playerIndex;
        player = GameManager.instance.players[playerIndex];
        SelectModelToUse();
    }

    #region basic movement
    public virtual void MoveTo(int square_id, bool voluntary = true)
    {
        square.RemovePiece();
        GameManager.instance.boardLogic.map.AddPieceToSquare(this, square_id); //this adds "square" to this as well
        hasMoved = true;
        transform.position = GameManager.instance.boardLogic.map.board[square_id].transform.position;
        GameManager.instance.moveTaken = true;
        GameNoticationCenter.TriggerEvent(GameEventTrigger.PieceMoved);
    }

    protected Square GetNeighbor(int neighborIndex, int numOfSpaces)
    {
        Square selectedSquare = square;
        for (int i = 0; i < numOfSpaces; i++)
        {
            Square next = selectedSquare.neighbors[neighborIndex];
            if (next == null)
                return null;
            selectedSquare = next;
        }
        return selectedSquare;
    }

    #endregion
    #region capture
    public void CaptureEnemyPiece(Piece_Base targetPiece, int target)
    {
        GameManager.instance.pieceCapturing = this;
        GameManager.instance.pieceBeingCaptured = targetPiece;
        GameLog.AddText(displayName + " captured " + targetPiece.displayName + " at " + targetPiece.square.UniqueID.ToString() + "\n");

        player.noticationCenter.TriggerEvent(EventToTrigger.EnemyCaptured);

        player.capturedPieces.Add(targetPiece as Piece);
        (targetPiece as Piece).OnCaptured();

        if (moveWithCapture || target == -1)
        {
            MoveTo(target);
        }
        else
        {
            GameManager.instance.moveTaken = true;
            hasMoved = true;
        }
        UnSelect();
    }

    public virtual bool CanBeDestroyedBy(Piece piece)
    {
        return piece.playerIndex != playerIndex;
    }

    #endregion

    #region visual
    public GameObject[] models; //set in editor; character models attached to this gameobject ( "White is index 1; black index 0")
    protected abstract void UnSelect();

    //Destory unused character models for this piece
    void SelectModelToUse()
    {
        //TODO: selected model should come from armylist object
        int modelIndexToKeepActive = 0;
        if (playerIndex <= models.Length)
        {
            modelIndexToKeepActive = playerIndex;
        }
        for( int i =0; i<models.Length; i++)
        {
            if( i != modelIndexToKeepActive)
            {
                Destroy(models[i]);
            }
        }
        //TODO: Ensure model is rotated correctly
    }
    #endregion
}
