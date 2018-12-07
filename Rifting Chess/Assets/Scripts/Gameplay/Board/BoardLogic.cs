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

        AddPiece(LoadManager.whiteRook, 0, 0, 0);
        AddPiece(LoadManager.whiteKnight, 0, 1, 0);
        AddPiece(LoadManager.whiteBishop, 0, 2, 0);
        AddPiece(LoadManager.whiteQueen, 0, 3, 0);
        AddPiece(LoadManager.whiteKing, 0, 4, 0);
        AddPiece(LoadManager.whiteBishop, 0, 5, 0);
        AddPiece(LoadManager.whiteKnight, 0, 6, 0);
        AddPiece(LoadManager.whiteRook, 0, 7, 0);

        for (int i = 0; i < 8; i++){
            AddPiece(LoadManager.whitePawn, 0, i, 1);
        }
        AddPiece(LoadManager.blackRook, 1, 0, 7);
        AddPiece(LoadManager.blackKnight, 1, 1, 7);
        AddPiece(LoadManager.blackBishop, 1, 2, 7);
        AddPiece(LoadManager.blackQueen, 1, 3, 7);
        AddPiece(LoadManager.blackKing, 1, 4, 7);
        AddPiece(LoadManager.blackBishop, 1, 5, 7);
        AddPiece(LoadManager.blackKnight, 1, 6, 7);
        AddPiece(LoadManager.blackRook, 1, 7, 7);

        for (int i = 0; i < 8; i++){
            AddPiece(LoadManager.blackPawn, 1, i, 6);
        }

        GameManager.instance.PiecesAdded();
    }

    public void AddPiece(String prefabLocation, int player, int col, int row)
    {
        AddPiece((GameObject)Resources.Load(prefabLocation, typeof(GameObject)), player, col, row) ;
        /*Piece piece = appearance.AddPiece((GameObject)Resources.Load(prefabLocation, typeof(GameObject)), col, row).GetComponent<Piece>();
        GameManager.instance.AddPieceToPlayer(player, piece);
        piece.playerIndex = player;
        map.AddPieceToSquare(piece, new Vector2Int(col, row));
        piece.square = map.SquareAt(col, row);*/
    }

    public void AddPiece(GameObject pieceObject, int player , int col, int row) {
        Piece piece = appearance.AddPiece(pieceObject, col, row).GetComponent<Piece>();
        GameManager.instance.AddPieceToPlayer(player, piece);
        piece.playerIndex = player;
        map.AddPieceToSquare(piece, new Vector2Int(col, row));
        piece.square = map.SquareAt(col, row);
    }

    public GameObject GetPieceTypeForPlayer( PieceType type, Player player){
        int index = GameManager.instance.players[0] == player ? 0 : 1;
        switch (type){
            case PieceType.Bishop:
                return index == 0 ? LoadManager.whiteBishop : LoadManager.blackBishop;
            case PieceType.King:
                return index == 0 ? LoadManager.whiteKing : LoadManager.blackKing;
            case PieceType.Knight:
                return index == 0 ? LoadManager.whiteKnight : LoadManager.blackKnight;
            case PieceType.Pawn:
                return index == 0 ? LoadManager.whitePawn : LoadManager.blackPawn;
            case PieceType.Queen:
                return index == 0 ? LoadManager.whiteQueen : LoadManager.blackQueen;
            case PieceType.Rook:
                return index == 0 ? LoadManager.whiteRook : LoadManager.blackRook;
            default:
                Debug.Log("Error finding piece " + type.ToString());
                return LoadManager.whiteBishop;
           }
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
