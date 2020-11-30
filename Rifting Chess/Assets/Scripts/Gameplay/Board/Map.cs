using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map  {

    public Square[] board;

    public Map(Square[] mapInfo) 
    {
        board = mapInfo;

        for( int i = 0; i < board.Length; i++)
        {
            board[i].SetUID(i);
        }
    }

    public void AddPieceToSquare( Piece_Base piece, int square_id ) { 
        board[square_id].AddPiece(piece as Piece);//this adds "square" to this as well
    }

    public Square SquareAt( int square_id ) {
        return board[square_id];
    }

    public void RemoveThreats(){
        foreach (Square square in board)
        {
            square.RemoveThreat();
        }
    }

    public void Threaten(List<int> piece_IDs , int playerNumber){
        foreach (int id in piece_IDs) {
            board[id].AddThreat(playerNumber);
        }
    }
}
