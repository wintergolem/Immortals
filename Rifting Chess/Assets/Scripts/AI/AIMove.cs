using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove  {

    public Piece piece;
    public Vector2Int destination;
    public int weight = 0;

    public AIMove(Piece p, Vector2Int l){
        piece = p;
        destination = l;
    }
}