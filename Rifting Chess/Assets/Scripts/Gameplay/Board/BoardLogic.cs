using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum BoardState {TileSelect, MoveSelect, SummonSelect }; 

public class BoardLogic : MonoBehaviour
{
    public bool castleMovePossible = false;
    public Map map;
    //public Piece movingPiece;
    
    private List<int> moveLocations;
    private BoardAppearance appearance;
  

    #region GAME SETUP METHODS
    void Start() {

        map = new Map(GetComponent<MapInfo>().board);
        appearance = GetComponent<BoardAppearance>();
    }

     public void AddPiece(GameObject pieceObject, int player , int square_ID) 
     {
         AddPiece(pieceObject, player, map.board[square_ID]) ;
     }

    public void AddPiece(GameObject pieceObject, int player , Square targetSquare) {
        Piece piece = appearance.AddPiece(pieceObject, targetSquare).GetComponent<Piece>();
        GameManager.instance.AddPieceToPlayer(player, piece);
        piece.AssignPlayer(player);
        GameManager.instance.SetupNewPiece(piece, player);

        map.AddPieceToSquare(piece, targetSquare.UniqueID);
        piece.square = targetSquare;
    }
    #endregion

    #region To Appearance Methods
    public void MouseOver(){
        appearance.MouseOver(InputManager.lastSquareTouched);
    }
    public void RemoveMouseOver() {
        appearance.RemoveMouseOver();
    }

    public void SelectPiece(Piece piece) {
        appearance.SelectPiece(piece.gameObject);
        //movingPiece = piece;
        SetMoveLocation(piece);
        appearance.PlaceMoveIndicators(moveLocations);
    }

    public void SquaresWithIndicators(List<int> locations, bool movementSquares)
    {
        if (movementSquares)
            appearance.PlaceMoveIndicators(locations);
        else
            appearance.PlaceThreatIndicators(locations);
    }

    public void RemoveAllIndicators()
    {
        appearance.RemoveAllIndicators();
    }

    public void RemoveSpecificIndicators(List<int> locations)
    {
        appearance.RemoveIndicatorSpecific(locations);
    }
    public void DeselectPiece(Piece piece) 
    {
        appearance.DeselectPiece(piece.gameObject);
        //movingPiece = null;
        appearance.RemoveAllIndicators();
    }

    public void ChangeHighlightOfPiece(Piece piece , bool unHighLight = false)
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

    public void SelectPieceAtSquare(int square_id) 
    {
        Piece selectedPiece = map.SquareAt(square_id).piece;
        if (selectedPiece) 
        {
            appearance.SelectPiece(selectedPiece.gameObject);
        }
    }

    public Piece PieceOnSquare(int square_id)
    {
        return map.SquareAt(square_id).piece;
    }

    public List<int> MovesForPiece(Piece piece)  
    {
        var locations = piece.MoveLocations;
        return locations;
    }

    public List<AIMove> MovesForAIPiece(Piece piece)
     {
        List<AIMove> moves = new List<AIMove>();
        var locations = piece.MoveLocations;
        foreach (int l in locations){
            AIMove move = new AIMove(piece,l);
            moves.Add(move);
        }
        return moves;
    }

    public bool SquareValidMoveTarget(int square_id) 
    {
        return moveLocations.Contains(square_id);
    }

    public List<int> GetEmptySpawnPointsForPlayer(int playerIndex)
    {
        List<int> returnValue = new List<int>();
        foreach( Square square in map.board)
        {
            if( square.IsSpawnPoint(playerIndex) && square.IsEmpty)
            {
                returnValue.Add(square.UniqueID);
            }
        }
        return returnValue;
    }

    #endregion

    #region MOVELOCATION METHODS
    public List<int> GetMoveLocations()
    {
        return moveLocations;
    }
    public void SetMoveLocation(Piece piece)
    {
        moveLocations = MovesForPiece(piece);
    }
    #endregion
}
