using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Zombies : Faction {

    public string pieceToSummonPath;

    public Zombies(){
        type = FactionType.Zombies;
        bishopBlackLocation = "Prefabs/ZombieFaction/BishopBlack";
        kingBlackLocation       = "Prefabs/ZombieFaction/KingBlack";
        knightBlackLocation     = "Prefabs/ZombieFaction/KnightBlack";
        pawnBlackLocation   = "Prefabs/ZombieFaction/PawnBlack";
        pawnWhiteLocation   = "Prefabs/ZombieFaction/PawnWhite";
        queenBlackLocation = "Prefabs/ZombieFaction/QueenBlack";
        rookBlackLocation = "Prefabs/ZombieFaction/RookBlack";
    }

    public PostMoveAction CheckSummon(BoardLogic boardLogic, Player active , PieceType attackingPieceType, PieceType capturedPieceType) {
        Piece king = active.GetKing();
        if (boardLogic.MovesForPiece(king).Count == 0) {
            return PostMoveAction.None;
        }else {
            if (attackingPieceType == PieceType.Queen){
                pieceToSummonPath = boardLogic.GetPieceTypeForPlayer(capturedPieceType, active);
                hasPreMoveAction = true;
                preMoveActionType = PreMoveActionType.Summon;
                return PostMoveAction.None;
            }else {
                pieceToSummonPath = boardLogic.GetPieceTypeForPlayer(PieceType.Pawn, active);
                boardLogic.SelectPiece(king);
                return PostMoveAction.Summon;
            }
        }
    }
}