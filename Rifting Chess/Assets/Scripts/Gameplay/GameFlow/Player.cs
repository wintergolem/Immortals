using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { Local, AI, Network }

public class Player
{
    public List<Piece> pieces;
    public List<Piece> capturedPieces;

    public PlayerNoticationCenter noticationCenter;

    public string name;
    public int forward;
    public FactionType faction;
    public PlayerType type;
    public int playerNumber;
    public GameObject endDialog;

    public Player(string name, bool positiveZMovement , PlayerType playerType, int playerNumber)
    {
        this.name = name;
        pieces = new List<Piece>();
        capturedPieces = new List<Piece>();
        type = playerType;
        this.playerNumber = playerNumber;

        forward = positiveZMovement ? 1 : -1;

        noticationCenter = forward > 0 ? PlayerNoticationCenter.playerOne : PlayerNoticationCenter.playerTwo;

        this.faction = FactionType.Undead; //placeholder
    }

    public Piece GetKing() {
        foreach (Piece p in pieces) {
            if (p.type == PieceType.King) {
                return p;
            }
        }
        //Debug.Log("No king found for " + name + " player");
        return null;
    }

}
