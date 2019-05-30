using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArmyList {
    public List<PieceInfo> pieces = new List<PieceInfo>();

    public FactionType faction ;
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
        faction = FactionType.Undead;
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
}
