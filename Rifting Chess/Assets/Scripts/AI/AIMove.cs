using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove  {

    public Piece piece;
    public int destination; //square_id of destination
    public int weight = 0;

    public AIMove(Piece p, int piece_id){
        piece = p;
        destination = piece_id;
    }
}