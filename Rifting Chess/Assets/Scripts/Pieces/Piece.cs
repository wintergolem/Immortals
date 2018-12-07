using System.Collections.Generic;
using UnityEngine;
using System;

public enum PieceType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Piece : MonoBehaviour
{
    public PieceType type;
    public int playerIndex;
    public Square square;
    public bool hasMoved = false;
    public bool moveWithCapture = true;
    public int value = 0;
    public string displayName;

    public Action startOfTurnAction;
    public Action endOfTurnAction;
    public Action friendlyCaptured;
    public Action enemyCaptured;

    public virtual void CheckMap(){
        return;
    }

    protected int[] RookIndexes = {0,2,4,6 };
    protected int[] BishopIndexes = { 1, 3, 5, 7 };

    public abstract List<Vector2Int> MoveLocations(Vector2Int gridPoint);
    public virtual List<Vector2Int> GetThreatLocations() {
        return MoveLocations(square.personalCoord);
    }

    //checks numOfSpaces in neighborIndex direction and returns if they are empty
    protected bool CheckNeighors(int neighborIndex, int numOfSpaces)
    {
        Square selectedSquare = square;
        for (int i = 0; i < numOfSpaces; i++)
        {
            Square next = selectedSquare.neighbors[neighborIndex];
            if (next.Empty == false)
                return false;
            else
                selectedSquare = next;
        }
        return true;
    }

    protected Square GetNeighbor(int neighborIndex, int numOfSpaces)
    {
        Square selectedSquare = square;
        for (int i = 0; i < numOfSpaces; i++)
        {
            Square next = selectedSquare.neighbors[neighborIndex];
            if (next == null)
                return null;
            selectedSquare = next;
        }
        return selectedSquare;
    }

    public void DestoryPawn(){
        Destroy(GetComponent<Pawn>());
    }
}
