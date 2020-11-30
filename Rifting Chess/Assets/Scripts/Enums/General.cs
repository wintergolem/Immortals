using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType {  Undead, Priest, Phalanx, Warrior, Base };

public static class FactionTypeFunc
{
    public static FactionType[] GetArray()
    {
        return new FactionType[5] { FactionType.Phalanx, FactionType.Priest, FactionType.Undead, FactionType.Warrior, FactionType.Base };
    }
}