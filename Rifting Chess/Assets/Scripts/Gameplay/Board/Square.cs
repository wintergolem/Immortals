using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square  {

    public Piece piece;
    public Map map;
    public Vector2Int personalCoord;
    public Square[] neighbors; 
    // [7] = -x,+y ; [0] = x,+y ; [1] =  +x,+y
    //[6] = -x,y ;                     [2] = +x,y
    //[5] = -x;-y ; [4] = x,-y ;   [3] = +x,-y

    public bool[] bThreated = new bool[2];

    public bool Empty {
        get {
            return piece == null;
        }
    }
    public bool available = false;

    public Square( Map map , Vector2Int coord) {
        this.map = map;
        neighbors = new Square[4];
        this.personalCoord = coord;
    }

    public void RemovePiece() {
        piece = null;
    }

    public void AddPiece(Piece piece) {
        if( !Empty )
            throw new System.Exception("This is already a piece here: " + personalCoord.ToString());

        this.piece = piece;
    }

    public bool Threatened( int player){
        return bThreated[player];
    }

    public void RemoveThreat(){
        bThreated[0] = false;
        bThreated[1] = false;
    }
}
