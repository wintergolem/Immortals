using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map  {

    public Square[,] squares;
    public int rowCount = 8;
    public int columnCount = 8;

    public Map() {
        squares = new Square[8, 8];
        //fill array
        for (int x = 0; x < rowCount; x++) {
            for (int y = 0; y < columnCount; y++) {
                //Debug.Log("x = " + x.ToString() + " y = " + y.ToString());
                squares[x,y] = new Square(this, new Vector2Int(x,y));
            }
        }

        //attach neighbors
        for (int x = 0; x < 8; x++) //x
        {
            for (int y = 0; y < 8; y++) //y
            {
                Square[] array = new Square[8];
                if (x != 0 && y != 7)
                    array[7] = squares[x-1, y+1];
                if (y != 7) 
                    array[0] = squares[x, y+1];
                if (x != 7 && y != 7)
                    array[1] = squares[x+1, y+1];

                if (x != 0)
                    array[6] = squares[x-1, y];
                if (x != 7)
                    array[2] = squares[x+1, y];

                if (x != 0 && y != 0)
                    array[5] = squares[x - 1, y - 1];
                if (y != 0)
                    array[4] = squares[x , y-1];
                if (x != 7 && y != 0)
                    array[3] = squares[x + 1, y - 1];
                //Debug.Log("x = " + x.ToString() + " y = " + y.ToString());
                squares[x, y].neighbors = array;
            }
        }
    }

    public void AddPieceToSquare( Piece piece, Vector2Int coord ) {
        squares[coord.x, coord.y].piece = piece;
    }

    public Square SquareAt( Vector2Int coord ) {
        return SquareAt(coord.x, coord.y);
    }
    public Square SquareAt( int x , int y) {
        if ( x < 0 || x > squares.GetLongLength(0) - 1 || y < 0 || y > squares.GetLongLength(1) - 1 ) {
            return null;
        }
        return squares[x, y];
    }

    public void RemoveThreats(){
        foreach (Square square in squares){
            square.RemoveThreat();
        }
    }

    public void Threaten(List<Vector2Int> locations , int playerNumber){
        foreach (Vector2Int loc in locations) {
            SquareAt(loc).bThreated[playerNumber] = true;
        }
    }
}
