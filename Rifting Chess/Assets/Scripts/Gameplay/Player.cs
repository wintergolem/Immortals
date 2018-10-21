using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { Local, AI, Network }

public class Player
{
    public List<Piece> pieces;
    public List<Piece> capturedPieces;

    public string name;
    public int forward;
    public Faction faction;
    public PlayerType type;
    public int playerNumber;

    public Player(string name, bool positiveZMovement , FactionType faction, PlayerType playerType, int playerNumber)
    {
        this.name = name;
        pieces = new List<Piece>();
        capturedPieces = new List<Piece>();
        type = playerType;
        this.playerNumber = playerNumber;

        forward = positiveZMovement ? 1 : -1;
        //Debug.Log("Writing factions");
        switch (faction){
            case FactionType.Zombies:
                this.faction = new Zombies();
                break;
            case FactionType.Priest:
                this.faction = new Priest();
                break;
            case FactionType.Phalanx:
                this.faction = new Phalanx();
                break;
        }
    }

    public Piece GetKing() {
        foreach (Piece p in pieces) {
            if (p.type == PieceType.King) {
                return p;
            }
        }
        Debug.Log("No king found for " + name + " player");
        return null;
    }

}
