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
        print("logic - Start");
        map = new Map();
        InitialSetup( GameManager.instance.players);
    }

    public void InitialSetup( Player[] players )
    {
        print("logic - initialSetup");
        appearance = GetComponent<BoardAppearance>();

        AddPiece(players[0].faction.rookWhiteLocation, 0, 0, 0);
        AddPiece(players[0].faction.knightWhiteLocation, 0, 1, 0);
        AddPiece(players[0].faction.bishopWhiteLocation, 0, 2, 0);
        AddPiece(players[0].faction.queenWhiteLocation, 0, 3, 0);
        AddPiece(players[0].faction.kingWhiteLocation, 0, 4, 0);
        AddPiece(players[0].faction.bishopWhiteLocation, 0, 5, 0);
        AddPiece(players[0].faction.knightWhiteLocation, 0, 6, 0);
        AddPiece(players[0].faction.rookWhiteLocation, 0, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(players[0].faction.pawnWhiteLocation, 0, i, 1);
        }
        AddPiece(players[1].faction.rookBlackLocation, 1, 0, 7);
        AddPiece(players[1].faction.knightBlackLocation, 1, 1, 7);
        AddPiece(players[1].faction.bishopBlackLocation, 1, 2, 7);
        AddPiece(players[1].faction.queenBlackLocation, 1, 3, 7);
        AddPiece(players[1].faction.kingBlackLocation, 1, 4, 7);
        AddPiece(players[1].faction.bishopBlackLocation, 1, 5, 7);
        AddPiece(players[1].faction.knightBlackLocation, 1, 6, 7);
        AddPiece(players[1].faction.rookBlackLocation, 1, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(players[1].faction.pawnBlackLocation, 1, i, 6);
        }
    }

    public void AddPiece(String prefabLocation, int player, int col, int row)
    {
        Piece piece = appearance.AddPiece((GameObject)Resources.Load(prefabLocation, typeof(GameObject)), col, row).GetComponent<Piece>();
        GameManager.instance.AddPieceToPlayer(player, piece);
        piece.playerIndex = player;
        map.AddPieceToSquare(piece, new Vector2Int(col, row));
        piece.square = map.SquareAt(col, row);
    }

    public string GetPieceTypeForPlayer( PieceType type, Player player){
        int index = GameManager.instance.players[0] == player ? 0 : 1;
        string path = "Prefabs/Basic/KingBlack";
        switch (type){
            case PieceType.Bishop:
                return index == 0 ? player.faction.bishopWhiteLocation : player.faction.bishopBlackLocation;
            case PieceType.King:
                return index == 0 ? player.faction.kingWhiteLocation : player.faction.kingBlackLocation;
            case PieceType.Knight:
                return index == 0 ? player.faction.knightWhiteLocation : player.faction.knightBlackLocation;
            case PieceType.Pawn:
                return index == 0 ? player.faction.pawnWhiteLocation : player.faction.pawnBlackLocation;
            case PieceType.Queen:
                return index == 0 ? player.faction.queenWhiteLocation : player.faction.queenBlackLocation;
            case PieceType.Rook:
                return index == 0 ? player.faction.rookWhiteLocation : player.faction.rookBlackLocation;
            default:
                Debug.Log("Error finding piece " + type.ToString());
                return path;
           }
        //return (GameObject)Resources.Load(path,typeof(GameObject));
    }
    #endregion

    #region To Appearance Methods
    public void MouseOver(Vector2Int gridPoint){
        appearance.MouseOver(gridPoint);
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

    public void DeselectPiece(Piece piece) {
        appearance.DeselectPiece(piece.gameObject);
        movingPiece = null;
        appearance.RemoveMoveHighlights();
    }

    public void HighlightPiece(Piece piece , bool unHighLight = false){
        if ( unHighLight )
            appearance.DeselectPiece(piece.gameObject);
        appearance.SelectPiece(piece.gameObject);
    }

    #endregion

    #region Gameplay Methods

    public void SelectPieceAtGrid(Vector2Int gridPoint) {
        Piece selectedPiece = map.SquareAt(gridPoint).piece;
        if (selectedPiece) {
            appearance.SelectPiece(selectedPiece.gameObject);
        }
    }

    public Piece PieceAtGrid(Vector2Int gridPoint)
    {
        return gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0 ? null : map.SquareAt(gridPoint.x, gridPoint.y).piece;
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint) {
        if (map.SquareAt(gridPoint).Empty)
            return false;
        Piece selectedPiece = map.SquareAt(gridPoint).piece;
        return selectedPiece.playerIndex == GameManager.instance.ActivePlayerIndex;
    } 

    public void Move(Piece piece, Vector2Int gridPoint) {
        piece.square.RemovePiece(); //disconnect moving piece from square
        map.squares[gridPoint.x, gridPoint.y].AddPiece(piece); //connect moving to new square
        piece.square = map.SquareAt(gridPoint); //connect square to piece
        piece.hasMoved = true;
        appearance.MovePiece(piece.gameObject, gridPoint);
    }

    public void CapturePieceAt(Vector2Int gridPoint) {
        Piece pieceToCapture = map.SquareAt(gridPoint).piece;
        GameManager.instance.activePlayer.capturedPieces.Add(pieceToCapture);
        GameManager.instance.inactivePlayer.pieces.Remove(pieceToCapture);
        map.SquareAt(gridPoint).RemovePiece();
        Destroy(pieceToCapture.gameObject);
    }

    public List<Vector2Int> MovesForPiece(Piece piece)  {
        Vector2Int gridPoint = piece.square.personalCoord;
        var locations = piece.MoveLocations(gridPoint);
        locations.RemoveAll(FriendlyPieceAt);
        return locations;
    }

    public List<AIMove> MovesForAIPiece(Piece piece) {
        List<AIMove> moves = new List<AIMove>();
        var locations = piece.MoveLocations(piece.square.personalCoord);
        locations.RemoveAll(FriendlyPieceAt);
        foreach (Vector2Int l in locations){
            AIMove move = new AIMove(piece,l);
            moves.Add(move);
        }
        return moves;
    }

    public bool GridPointValidMoveTarget(Vector2Int gridPoint) {
        return moveLocations.Contains(gridPoint);
    }

    #endregion
}
