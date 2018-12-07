using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPhalanx : Pawn {

    public bool inPhalanx = false;

    public PawnPhalanx()
    {
        startOfTurnAction = CheckMap;
    }

    public override void CheckMap() {
        //check left
        if (square.personalCoord.x != 7 &&  !square.neighbors[2].Empty && square.neighbors[2].piece.type == PieceType.Pawn){
            inPhalanx = (square.personalCoord.x != 6 && !square.neighbors[2].neighbors[2].Empty && square.neighbors[2].neighbors[2].piece.type == PieceType.Pawn)
                || (square.personalCoord.x != 0 && !square.neighbors[6].Empty && square.neighbors[6].piece.type == PieceType.Pawn);
        } else {inPhalanx = false;}
        if (!inPhalanx)  //check right two spaces
        if (square.personalCoord.x > 1 && !square.neighbors[6].Empty && square.neighbors[6].piece.type == PieceType.Pawn) {
                inPhalanx = !square.neighbors[6].neighbors[6].Empty && square.neighbors[6].neighbors[6].piece.type == PieceType.Pawn;
        }

        moveWithCapture = !inPhalanx;
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }
}
