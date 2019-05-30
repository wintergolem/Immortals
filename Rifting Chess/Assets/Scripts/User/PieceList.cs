using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PieceList {

    public static List<PieceInfo> allPieces = new List<PieceInfo> {

          //UNDEAD FACTION
           //Base Set
            //Pawns
            new PieceInfo("Prefabs/Pieces/ZombieFaction/Pawn/Z-P-W-Standard", "Prefabs/Pieces/ZombieFaction/Pawn/Z-P-B-Standard", "Zombie", new string[0], 1, FactionType.Undead, PieceType.Pawn),
            //bishops
            new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/ZombieFaction/Bishop/Z-B-B-Standard", "Unholy Bishop", new string[0], 3, FactionType.Undead, PieceType.Bishop),
            //Rooks
            new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/ZombieFaction/Rook/Z-R-B-Standard", "Undead Rook", new string[0], 5, FactionType.Undead, PieceType.Rook),
            //knights
            new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/ZombieFaction/Knight/Z-KN-B-Standard", "Undead Knight", new string[0], 3, FactionType.Undead, PieceType.Knight),
             
           //Empowered Set
            //Pawns
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Charging", "Prefabs/Pieces/ZombieFaction/Pawn/Z-P-B-Charging", "Charging Pawn", new string[0], 1, FactionType.Undead, PieceType.Pawn),
           new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Expert", "Prefabs/Pieces/Basic/Pawn/U-P-B-Expert", "Expert Pawn", new string[0], 1, FactionType.Undead, PieceType.Pawn),
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Feinting", "Prefabs/Pieces/Basic/Pawn/U-P-B-Feinting", "Feinting Pawn", new string[0], 1, FactionType.Undead, PieceType.Pawn),
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Stampeding", "Prefabs/Pieces/Basic/Pawn/U-P-B-Stampeding", "Stampeding Pawn", new string[0], 1, FactionType.Undead, PieceType.Pawn),
            //Rooks
            new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Murderhole", "Prefabs/Pieces/ZombieFaction/Rook/Z-R-B-Murderhole", "Murderhole Rook", new string[0], 6, FactionType.Undead, PieceType.Rook),
            //Knight
              new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-BruteForce", "Prefabs/Pieces/Basic/Knight/U-KN-B-Bruteforce", "BruteForce Knight", new string[0], 6, FactionType.Undead, PieceType.Knight),
               //Queen
              new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Knighted", "Prefabs/Pieces/Basic/Queen/U-Q-B-Knighted", "Knighted Queen", new string[0], 6, FactionType.Undead, PieceType.Queen),

          //Necromancer Set
        //pawns
         new PieceInfo("Prefabs/Pieces/ZombieFaction/Pawn/Z-P-W-BerserkingZombie", "Prefabs/Pieces/ZombieFaction/Pawn/Z-P-B-BerserkingZombie", "BerserkingZombie", new string[0], 2, FactionType.Undead, PieceType.Pawn),
        //champions
        new PieceInfo("Prefabs/Pieces/ZombieFaction/Queen/Z-Q-W-Necromistress", "Prefabs/Pieces/ZombieFaction/Queen/Z-Q-B-Necromistress", "Necromistress", new string[0], 10 , FactionType.Undead, PieceType.Queen),
        //leaders
        new PieceInfo("Prefabs/Pieces/ZombieFaction/King/Z-K-W-Arch_Necromancer", "Prefabs/Pieces/ZombieFaction/King/Z-K-W-Arch_Necromancer", "Arch-Necromancer(King)", new string[0], 0, FactionType.Undead, PieceType.King),



        //PHALANX FACTION
         //Empowered Set
             //Pawns
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Charging", "Prefabs/Pieces/Basic/Pawn/U-P-B-Charging", "Charging Pawn", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
              new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Stampeding", "Prefabs/Pieces/Basic/Pawn/U-P-B-Stampeding", "Stampeding Pawn", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
           new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Expert", "Prefabs/Pieces/Basic/Pawn/U-P-B-Expert", "Expert Pawn", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Feinting", "Prefabs/Pieces/Basic/Pawn/U-P-B-Feinting", "Feinting Pawn", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
            //Rooks
            new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Murderhole", "Prefabs/Pieces/Basic/Rook/U-R-B-Murderhole", "Murderhole Rook", new string[0], 6, FactionType.Phalanx, PieceType.Rook),
           //Knight
              new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-BruteForce", "Prefabs/Pieces/Basic/Knight/U-KN-B-Bruteforce", "BruteForce Knight", new string[0], 6, FactionType.Phalanx, PieceType.Knight),
               //Queen
              new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Knighted", "Prefabs/Pieces/Basic/Queen/U-Q-B-Knighted", "Knighted Queen", new string[0], 6, FactionType.Phalanx, PieceType.Queen),

             //Roman Set
         //pawn
        new PieceInfo("Prefabs/Pieces/PhalanxFaction/Pawn/P-P-W-PhalanxPawn", "Prefabs/Pieces/PhalanxFaction/Pawn/P-P-B-PhalanxPawn", "Phalanx", new string[0], 1, FactionType.Phalanx, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/Basic/Rook/U-R-B-Standard", "Rook", new string[0], 5, FactionType.Phalanx, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/Basic/Knight/U-KN-B-Standard", "Calvary (Knight)", new string[0], 3, FactionType.Phalanx, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/Basic/Bishop/U-B-B-Standard", "Bishop", new string[0], 3, FactionType.Phalanx, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/Basic/Queen/U-Q-B-Standard", "Queen", new string[0], 9, FactionType.Phalanx, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/Basic/King/U-K-W-Standard", "Prefabs/Pieces/Basic/King/U-K-B-Standard", "King", new string[0], 0, FactionType.Phalanx, PieceType.King),


        //PRIEST FACTION
         //Empowered Set
            //Pawns
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Charging", "Prefabs/Pieces/Basic/Pawn/U-P-B-Charging", "Charging Pawn", new string[0], 1, FactionType.Priest, PieceType.Pawn),
              new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Stampeding", "Prefabs/Pieces/Basic/Pawn/U-P-B-Stampeding", "Stampeding Pawn", new string[0], 1, FactionType.Priest, PieceType.Pawn),
           new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Expert", "Prefabs/Pieces/Basic/Pawn/U-P-B-Expert", "Expert Pawn", new string[0], 1, FactionType.Priest, PieceType.Pawn),
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Feinting", "Prefabs/Pieces/Basic/Pawn/U-P-B-Feinting", "Feinting Pawn", new string[0], 1, FactionType.Priest, PieceType.Pawn),
            //Rooks
            new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Murderhole", "Prefabs/Pieces/Basic/Rook/U-R-B-Murderhole", "Murderhole Rook", new string[0], 6, FactionType.Priest, PieceType.Rook),
             //Knight
              new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-BruteForce", "Prefabs/Pieces/Basic/Knight/U-KN-B-Bruteforce", "BruteForce Knight", new string[0], 6, FactionType.Priest, PieceType.Knight),
               //Queen
              new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Knighted", "Prefabs/Pieces/Basic/Queen/U-Q-B-Knighted", "Knighted Queen", new string[0], 6, FactionType.Priest, PieceType.Queen),

         //War Preacher Set
         //pawn
        new PieceInfo("Prefabs/Pieces/PriestFaction/Pawn/Pr-P-W-PriestPawn", "Prefabs/Pieces/PriestFaction/Pawn/Pr-P-B-PriestPawn", "Acolites (Pawn)", new string[0], 1, FactionType.Priest, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/Basic/Rook/U-R-B-Standard", "Rook", new string[0], 5, FactionType.Priest, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/Basic/Knight/U-KN-B-Standard", "Holy Knight", new string[0], 3, FactionType.Priest, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/Basic/Bishop/U-B-B-Standard", "Bishop", new string[0],3, FactionType.Priest, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/Basic/Queen/U-Q-B-Standard", "Queen", new string[0], 9, FactionType.Priest, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/PriestFaction/King/Pr-K-W-Standard", "Prefabs/Pieces/PriestFaction/King/Pr-K-B-Standard", "War Preacher (King)", new string[0], 0, FactionType.Priest, PieceType.King),

        //WARRIOR FACTION
                 //Empowered Set
            //Pawns
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Charging", "Prefabs/Pieces/Basic/Pawn/U-P-B-Charging", "Charging Pawn", new string[0], 1, FactionType.Warrior, PieceType.Pawn),
           new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Expert", "Prefabs/Pieces/Basic/Pawn/U-P-B-Expert", "Expert Pawn", new string[0], 1, FactionType.Warrior, PieceType.Pawn),
            new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Feinting", "Prefabs/Pieces/Basic/Pawn/U-P-B-Feinting", "Feinting Pawn", new string[0], 1, FactionType.Warrior, PieceType.Pawn),
              new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Stampeding", "Prefabs/Pieces/Basic/Pawn/U-P-B-Stampeding", "Stampeding Pawn", new string[0], 1, FactionType.Warrior, PieceType.Pawn),
            //Rooks
            new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Murderhole", "Prefabs/Pieces/Basic/Rook/U-R-B-Murderhole", "Murderhole Rook", new string[0], 6, FactionType.Warrior, PieceType.Rook),
             //Knight
              new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-BruteForce", "Prefabs/Pieces/Basic/Knight/U-KN-B-Bruteforce", "BruteForce Knight", new string[0], 6, FactionType.Warrior, PieceType.Knight),
               //Queen
              new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Knighted", "Prefabs/Pieces/Basic/Queen/U-Q-B-Knighted", "Knighted Queen", new string[0], 6, FactionType.Warrior, PieceType.Queen),

          //pawn
        new PieceInfo("Prefabs/Pieces/Basic/Pawn/U-P-W-Standard", "Prefabs/Pieces/Basic/Pawn/U-P-B-Standard", "Pawn", new string[0], 1, FactionType.Warrior, PieceType.Pawn),
        new PieceInfo("Prefabs/Pieces/WarriorFaction/Pawn/W-P-W-Warlust", "Prefabs/Pieces/WarriorFaction/Pawn/W-P-B-Warlust", "Warlust Pawn", new string[0], 2, FactionType.Warrior, PieceType.Pawn),
        //rook
        new PieceInfo("Prefabs/Pieces/Basic/Rook/U-R-W-Standard", "Prefabs/Pieces/Basic/Rook/U-R-B-Standard", "Rook", new string[0], 5, FactionType.Warrior, PieceType.Rook),
        //knight
        new PieceInfo("Prefabs/Pieces/Basic/Knight/U-KN-W-Standard", "Prefabs/Pieces/Basic/Knight/U-KN-B-Standard", "wKnight", new string[0], 3, FactionType.Warrior, PieceType.Knight),
        //bishop
        new PieceInfo("Prefabs/Pieces/Basic/Bishop/U-B-W-Standard", "Prefabs/Pieces/Basic/Bishop/U-B-B-Standard", "Bishop", new string[0], 3, FactionType.Warrior, PieceType.Bishop),
        //queen
        new PieceInfo("Prefabs/Pieces/Basic/Queen/U-Q-W-Standard", "Prefabs/Pieces/Basic/Queen/U-Q-B-Standard", "Queen", new string[0], 9, FactionType.Warrior, PieceType.Queen),
        //king
        new PieceInfo("Prefabs/Pieces/WarriorFaction/King/W-K-W-Standard", "Prefabs/Pieces/WarriorFaction/King/W-K-B-Standard", "Horselord (King)", new string[0], 0, FactionType.Warrior, PieceType.King),
        new PieceInfo("Prefabs/Pieces/Basic/King/U-K-W-Standard", "Prefabs/Pieces/Basic/King/U-K-B-Standard", "King", new string[0], 0, FactionType.Warrior, PieceType.King)





    };
}