using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfo {

    public string modelPath;
    public string displayName;
    public FactionType factionType;
    public int value;
    public string[] descriptions;
    public PieceType pieceType;

    public string hashID;

    public PieceInfo( string path, string name, string[] abilities, int value, FactionType faction, PieceType pieceType){
        modelPath = path;
        displayName = name;
        descriptions = abilities;
        this.value = value;
        factionType = faction;
        this.pieceType = pieceType;

        hashID = factionType.ToString().Remove(3) + displayName.Remove(3)  + pieceType.ToString().Remove(3) + value;
        //Debug.Log(hashID);
    }

    public PieceInfo(string path, string fakepath, string name, string[] abilities, int value, FactionType faction, PieceType pieceType)
    {
        modelPath = path;
        displayName = name;
        descriptions = abilities;
        this.value = value;
        factionType = faction;
        this.pieceType = pieceType;

        hashID = factionType.ToString().Remove(3) + displayName.Remove(3) + pieceType.ToString().Remove(3) + value;
        Debug.Log(name + " is still using old pieceInfo");
    }
}
