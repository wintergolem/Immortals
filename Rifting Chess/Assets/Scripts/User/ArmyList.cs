using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyList {
    public PieceInfo pawn;
    public PieceInfo rook;
    public PieceInfo bishop;
    public PieceInfo knight;
    public PieceInfo queen;
    public PieceInfo king;

    public Faction faction;
    public string displayName;

    public bool Complete(){
        return ( pawn != null && rook != null && bishop != null && knight != null && queen != null && king != null);
    }
    //public PieceInfo

    public bool AttemptAdd (PieceInfo info){
        switch (info.pieceType){
            case PieceType.Bishop:
                if (bishop == null){
                    bishop = info;
                    return true;
                }  break;
            case PieceType.King:
                if (king == null) {
                    king = info;
                    return true;
                }
                break;
            case PieceType.Knight:
                if (knight == null){
                    knight = info;
                    return true;
                }
                break;
            case PieceType.Pawn:
                if (pawn == null)   {
                    pawn = info;
                    return true;
                }
                break;
            case PieceType.Queen:
                if (queen == null){
                    queen = info;
                    return true;
                }
                break;
            case PieceType.Rook:
                if (rook == null){
                    rook = info;
                    return true;
                }
                break;
            default:
                return false; 
        }
        return false;
    }

    public void Remove(PieceType type){
        switch (type)  {
            case PieceType.Bishop:
                bishop = null;
                break;
            case PieceType.King:
                king = null;
                break;
            case PieceType.Knight:
                knight = null;
                break;
            case PieceType.Pawn:
                pawn = null;
                break;
            case PieceType.Queen:
                queen = null;
                break;
            case PieceType.Rook:
                rook = null;
                break;
            default:
                return;
        }
    }

    public static ArmyList BuildBasicZombie(){
        return new ArmyList
        {
            faction = new Zombies(),
            displayName = "Default",
            //pawn
            pawn = PieceList.allPieces[0],
            //rook
            rook = PieceList.allPieces[1],
            //knight
            knight = PieceList.allPieces[2],
            //bishop
            bishop = PieceList.allPieces[3],
            //queen
            queen = PieceList.allPieces[4],
            //king
            king = PieceList.allPieces[5],
        };
    }

    public static ArmyList BuildBasicPhalanx()
    {
        return new ArmyList
        {
            faction = new Phalanx(),
            displayName = "Default",
            //pawn
            pawn = PieceList.allPieces[6],
            //rook
            rook = PieceList.allPieces[7],
            //knight
            knight = PieceList.allPieces[8],
            //bishop
            bishop = PieceList.allPieces[9],
            //queen
            queen = PieceList.allPieces[10],
            //king
            king = PieceList.allPieces[11],
        };
    }

    public static ArmyList BuildBasicPriest()
    {
        return new ArmyList
        {
            faction = new Priest(),
            displayName = "Default",
            //pawn
            pawn = PieceList.allPieces[12],
            //rook
            rook = PieceList.allPieces[13],
            //knight
            knight = PieceList.allPieces[14],
            //bishop
            bishop = PieceList.allPieces[15],
            //queen
            queen = PieceList.allPieces[16],
            //king
            king = PieceList.allPieces[17],
        };
    }
}
