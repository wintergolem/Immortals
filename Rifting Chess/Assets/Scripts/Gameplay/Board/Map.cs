using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map  {

    public Square[,] squares;

    public Map(int[] squareCount) {
        squares = new Square[squareCount[0], squareCount[1]];
        //fill array
        for (int x = 0; x < squareCount[0]; x++) {
            for (int y = 0; y < squareCount[1]; y++) {
                squares[x,y] = new Square(this, new Vector2Int(x,y));
            }
        }

        //attach neighbors
        int maxX = squareCount[0] -1 ;
        int maxY = squareCount[1] -1 ;
        for (int x = 0; x < squareCount[0]; x++) //x
        {
            for (int y = 0; y < squareCount[1];  y++) //y
            {
                Square[] array = new Square[8];
                if (y != maxY)
                {
                    array[0] = squares[x, y + 1];
                    if (x != 0 )
                        array[7] = squares[x - 1, y + 1];
                    if (x != maxX)
                        array[1] = squares[x + 1, y + 1];
                }

                if (x != 0)
                    array[6] = squares[x-1, y];
                if (x != maxX)
                    array[2] = squares[x+1, y];

                if (y != 0)
                {
                    array[4] = squares[x, y - 1];
                    if (x != 0 )
                        array[5] = squares[x - 1, y - 1];
                    if (x != maxX)
                        array[3] = squares[x + 1, y - 1];
                }
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
