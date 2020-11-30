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

        Square left = square.neighbors[6];
        Square right = square.neighbors[2];

        inPhalanx = (left != null && !left.IsEmpty && left.piece.displayName == this.displayName) && (right != null && !right.IsEmpty && right.piece.displayName == displayName);

        moveWithCapture = !inPhalanx;
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    protected override void CalculateThreatLocations()
    {
        base.CalculateThreatLocations();

        if( inPhalanx )
        {
            int forwardIndex = ForwardDirection > 0 ? 0 : 4;
            Square forwardSquare = square.neighbors[forwardIndex];
            if( forwardSquare != null && !forwardSquare.IsEmpty && forwardSquare.piece.CanBeDestroyedBy(this))
            {
                ThreatWithPieces.Add(forwardSquare.UniqueID);
            }
        }
    }
}
