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
    }

    #region basic movement
    public virtual void MoveTo(Vector2Int space, bool voluntary = true)
    {
        square.RemovePiece();
        GameManager.instance.boardLogic.map.squares[space.x, space.y].AddPiece(this); //this adds "square" to this as well
        hasMoved = true;
        transform.position = Geometry.PointFromGrid(space);
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
    public void CaptureEnemyPiece(Piece_Base targetPiece, Vector2Int target)
    {
        GameManager.instance.pieceCapturing = this;
        GameManager.instance.pieceBeingCaptured = targetPiece;
        GameLog.AddText(displayName + " captured " + targetPiece.displayName + " at " + targetPiece.square.personalCoord.ToString() + "\n");

        player.noticationCenter.TriggerEvent(EventToTrigger.EnemyCaptured);

        player.capturedPieces.Add(targetPiece as Piece);
        (targetPiece as Piece).OnCaptured();

        if (moveWithCapture || target == Vector2Int.zero)
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
    protected abstract void UnSelect();
    #endregion
}
