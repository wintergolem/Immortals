using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PieceList {

    public static List<PieceInfo> allPieces = new List<PieceInfo> {

          //Undead  pieces
        //pawn
        new PieceInfo("Prefabs/Pieces/ZombieFaction/Pawn/Z-P-W-Standard", "Prefabs/Pieces/ZombieFaction/Pawn/Z-P-B-Standard", "Zombie", new string[0], 1, FactionType.Zombies, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/ZombieFaction/Rook/Z-R-B-Standard", "Rook", new string[0], 1, FactionType.Zombies, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/ZombieFaction/Knight/Z-KN-B-Standard", "Knight", new string[0], 1, FactionType.Zombies, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/ZombieFaction/Bishop/Z-B-B-Standard", "Bishop", new string[0], 1, FactionType.Zombies, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/ZombieFaction/Queen/Z-Q-B-Standard", "Queen", new string[0], 1, FactionType.Zombies, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/ZombieFaction/King/Z-K-W-Standard", "Prefabs/Pieces/ZombieFaction/King/Z-K-B-Standard", "King", new string[0], 1, FactionType.Zombies, PieceType.King),


        //Phalanx
         //pawn
        new PieceInfo("Prefabs/Pieces/PhalanxFaction/Pawn/P-P-W-PhalanxPawn", "Prefabs/Pieces/PhalanxFaction/Pawn/P-P-B-PhalanxPawn", "Phalanx", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/Basic/Rook/U-R-B-Standard", "Rook", new string[0], 1, FactionType.Phalanx, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/Basic/Knight/U-KN-B-Standard", "Knight", new string[0], 1, FactionType.Phalanx, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/Basic/Bishop/U-B-B-Standard", "Bishop", new string[0], 1, FactionType.Phalanx, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/Basic/Queen/U-Q-B-Standard", "Queen", new string[0], 1, FactionType.Phalanx, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/Basic/King/U-K-W-Standard", "Prefabs/Pieces/Basic/King/U-K-B-Standard", "King", new string[0], 1, FactionType.Phalanx, PieceType.King),


        //Priest
         //pawn
        new PieceInfo("Prefabs/Pieces/PriestFaction/Pawn/Pr-P-W-PriestPawn", "Prefabs/Pieces/PriestFaction/Pawn/Pr-P-B-PriestPawn", "PriestPawn", new string[0], 1, FactionType.Priest, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/Basic/Rook/U-R-B-Standard", "Rook", new string[0], 1, FactionType.Priest, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/Basic/Knight/U-KN-B-Standard", "Knight", new string[0], 1, FactionType.Priest, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/Basic/Bishop/U-B-B-Standard", "Bishop", new string[0], 1, FactionType.Priest, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/Basic/Queen/U-Q-B-Standard", "Queen", new string[0], 1, FactionType.Priest, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/PriestFaction/King/Pr-K-W-Standard", "Prefabs/Pieces/PriestFaction/King/Pr-K-B-Standard", "War Preacher (King)", new string[0], 1, FactionType.Priest, PieceType.King)

        //Warrior
    };
}