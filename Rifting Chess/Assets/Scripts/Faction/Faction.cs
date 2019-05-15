using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Faction  {
    public FactionType type;

    public static Faction FromType (FactionType type)
    {
        switch (type)
        {
            case FactionType.Phalanx:
                return new Phalanx();
            case FactionType.Priest:
                return new Priest();
            case FactionType.Warrior:
                return new Warrior();
            case FactionType.Zombies:
                return new Zombies();
            default:
                return new Zombies();
        }
    }
}
