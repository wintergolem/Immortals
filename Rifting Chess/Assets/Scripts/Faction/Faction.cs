using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Faction  {
    public FactionType type;

    //change to pieces
    public string pawnBlackLocation = "Prefabs/Basic/PawnBlack";
    public string kingBlackLocation = "Prefabs/Basic/KingBlack";
    public string knightBlackLocation = "Prefabs/Basic/KnightBlack";
    public string rookBlackLocation = "Prefabs/Basic/RookBlack";
    public string bishopBlackLocation = "Prefabs/Basic/BishopBlack";
    public string queenBlackLocation = "Prefabs/Basic/QueenBlack";

    public string pawnWhiteLocation = "Prefabs/Basic/PawnWhite";
    public string kingWhiteLocation = "Prefabs/Basic/KingWhite";
    public string knightWhiteLocation = "Prefabs/Basic/KnightWhite";
    public string rookWhiteLocation = "Prefabs/Basic/RookWhite";
    public string bishopWhiteLocation = "Prefabs/Basic/BishopWhite";
    public string queenWhiteLocation = "Prefabs/Basic/QueenWhite";

    //powers to attach to button
    public bool hasPowerToAttachToButton = false;
    public virtual void PowerAttachedToButton(){}

    //changes to game flow
    public bool hasPreMoveAction = false;
    public PreMoveActionType preMoveActionType = PreMoveActionType.None;
    public bool hasPostMoveAction = false;
    public PostMoveActionType postMoveActionType = PostMoveActionType.PowerReset;
    public bool hasCaptureReaction = false;
    public CaptureReactionType reactionType = CaptureReactionType.None;
   

    //basic functions all factions have
    public virtual void EndOfTurn(){
        return;
    }
}
