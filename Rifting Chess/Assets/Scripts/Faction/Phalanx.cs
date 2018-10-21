using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phalanx : Faction
{
    //change to pieces
    //public new string pawnBlackLocation = "Prefabs/PhalanxFaction/PawnBlack";
    //public new string pawnWhiteLocation = "Prefabs/PhalanxFaction/PawnWhite";

    public Phalanx(){

        type = FactionType.Phalanx;
        hasPreMoveAction = true;
        preMoveActionType = PreMoveActionType.CheckMap;
        hasPowerToAttachToButton = true;

        pawnBlackLocation = "Prefabs/PhalanxFaction/PawnBlack";
        pawnWhiteLocation = "Prefabs/PhalanxFaction/PawnWhite";
}
    public override void PowerAttachedToButton(){
        hasPowerToAttachToButton = false;
    }


}
