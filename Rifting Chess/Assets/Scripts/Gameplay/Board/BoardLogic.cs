using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum BoardState {TileSelect, MoveSelect, SummonSelect }; 

public class BoardLogic : MonoBehaviour
{
    public bool castleMovePossible = false;
    public Map map;
    public Piece movingPiece;
    public List<Vector2Int> moveLocations;

    private BoardAppearance appearance;
  

    #region GAME SETUP METHODS
    void Start() {

        map = new Map(GetComponent<MapInfo>().numberOfSquares);
        appearance = GetComponent<BoardAppearance>();
    }

    public void AddPiece(String prefabLocation, int player, int col, int row)
    {
        AddPiece((GameObject)Resources.Load(prefabLocation, typeof(GameObject)), player, col, row) ;
    }

    public void AddPiece(GameObject pieceObject, int player , int col, int row) {
        Piece piece = appearance.AddPiece(pieceObject, col, row).GetComponent<Piece>();
        GameManager.instance.AddPieceToPlayer(player, piece);
        piece.AssignPlayer(player);
        GameManager.instance.SetupNewPiece(piece, player);

        map.AddPieceToSquare(piece, new Vector2Int(col, row));
        piece.square = map.SquareAt(col, row);
    }
    #endregion

    #region To Appearance Methods
    public void MouseOver(){
        appearance.MouseOver(InputManager.lastGridPoint);
    }
    public void RemoveMouseOver() {
        appearance.RemoveMouseOver();
    }

    public void SelectPiece(Piece piece) {
        appearance.SelectPiece(piece.gameObject);
        movingPiece = piece;
        moveLocations = MovesForPiece(movingPiece);
        appearance.PlaceMoveHighlights(moveLocations);
    }

    public void HighlightSquares(List<Vector2Int> locations, bool movementSquares)
    {
        if (movementSquares)
            appearance.PlaceMoveHighlights(locations);
        else
            appearance.PlaceThreatenHighlights(locations);
    }

    public void UnhighlightSquares()
    {
        appearance.RemoveMoveHighlights();
    }

    public void UnhighlightSquares(List<Vector2Int> locations)
    {
        appearance.RemoveMoveHighlights(locations);
    }
    public void DeselectPiece(Piece piece) 
    {
        appearance.DeselectPiece(piece.gameObject);
        movingPiece = null;
        appearance.RemoveMoveHighlights();
    }

    public void HighlightPiece(Piece piece , bool unHighLight = false)
    {
        if (unHighLight)
        {
            appearance.DeselectPiece(piece.gameObject);
        }
        else
        {
            appearance.SelectPiece(piece.gameObject);
        }
    }

    #endregion

    #region Gameplay Methods

    public void SelectPieceAtGrid(Vector2Int gridPoint) 
    {
        Piece selectedPiece = map.SquareAt(gridPoint).piece;
        if (selectedPiece) 
        {
            appearance.SelectPiece(selectedPiece.gameObject);
        }
    }

    public Piece PieceAtGrid(Vector2Int gridPoint)
    {
        return gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0 ? null : map.SquareAt(gridPoint.x, gridPoint.y).piece;
    }

    public List<Vector2Int> MovesForPiece(Piece piece)  
    {
        var locations = piece.MoveLocations;
        return locations;
    }

    public List<AIMove> MovesForAIPiece(Piece piece)
     {
        List<AIMove> moves = new List<AIMove>();
        var locations = piece.MoveLocations;
        foreach (Vector2Int l in locations){
            AIMove move = new AIMove(piece,l);
            moves.Add(move);
        }
        return moves;
    }

    public bool GridPointValidMoveTarget(Vector2Int gridPoint) 
    {
        return moveLocations.Contains(gridPoint);
    }

    public List<Vector2Int> GetEmptyBackRowSquares(int playerIndex)
    {
        var returnValue = new List<Vector2Int>();
        int backRowIndex = GameManager.instance.players[playerIndex].forward > 0 ? 0 : map.squares.GetLength(1);
        for (int i = 0; i < map.squares.GetLength(0); i++)
         {
            if (map.SquareAt(new Vector2Int(i, backRowIndex)).Empty)
            {
                returnValue.Add(new Vector2Int(i, backRowIndex) );
            }
        }
        return returnValue;
    }

    #endregion
}
