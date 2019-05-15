using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArmyList {
    public List<PieceInfo> pieces = new List<PieceInfo>();

    public Faction faction ;
    public string displayName;
    public int maxPoints = 40;
    public int ListPointsUsed {
        get
        {
            int returnValue = 0;
            foreach (PieceInfo piece in pieces)
            {
                returnValue += piece.value;
            }
            return returnValue;
        }
    }
    public bool HasKing
    {
        get
        {
            foreach (PieceInfo piece in pieces)
            {
                if (piece.pieceType == PieceType.King)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public ArmyList()
    {
        faction = new Zombies();
    }

    public bool AttemptAdd (PieceInfo info){
        int currentTotal = ListPointsUsed;
        if (currentTotal + info.value > maxPoints)
        {
            return false;
        }
        if (info.pieceType == PieceType.King)
        {
            foreach (PieceInfo p in pieces)
            {
                if (p.pieceType == PieceType.King)
                {
                    return false;
                }
            }
        }

        pieces.Add(info);
        return true;
    }

    public void Remove(PieceInfo info){
        pieces.Remove(info);
    }

    public static ArmyList BuildBasicZombie()
    {
        return new ArmyList
        {
            faction = new Zombies(),
            displayName = "Default",
            pieces = new List<PieceInfo>{
            //pawn
            PieceList.allPieces[0] ,
            //rook
          PieceList.allPieces[1],
            //knight
           PieceList.allPieces[2],
            //bishop
           PieceList.allPieces[3],
            //queen
            PieceList.allPieces[4],
            //king
           PieceList.allPieces[5]
            }
        };

    }

    public static ArmyList BuildBasicPhalanx()
    {
        return new ArmyList
        {
            faction = new Phalanx(),
            displayName = "Default",
            pieces = new List<PieceInfo>{
            //pawn
            PieceList.allPieces[6],
            //rook
            PieceList.allPieces[7],
            //knight
             PieceList.allPieces[8],
            //bishop
            PieceList.allPieces[9],
            //queen
            PieceList.allPieces[10],
            //king
             PieceList.allPieces[11],
            }
        };
    }

    public static ArmyList BuildBasicPriest()
    {
        return new ArmyList
        {
            faction = new Priest(),
            displayName = "Default",
            pieces = new List<PieceInfo>
            {
                //pawn
                PieceList.allPieces[12],
                //rook
                PieceList.allPieces[13],
                //knight
                PieceList.allPieces[14],
                //bishop
                 PieceList.allPieces[15],
                //queen
                PieceList.allPieces[16],
                //king
               PieceList.allPieces[17],
            }
        };
    }

    public static ArmyList BuildBasicWarrior()
    {
        return new ArmyList
        {
            faction = new Warrior(),
            displayName = "Default",
            pieces = new List<PieceInfo>
            {
                //pawn
                PieceList.allPieces[18],
                //rook
                PieceList.allPieces[19],
                //knight
                PieceList.allPieces[20],
                //bishop
                PieceList.allPieces[21],
                //queen
                PieceList.allPieces[22],
                //king
                PieceList.allPieces[23],
            }
        };
    }
}
