using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]


public class ArmyListGoBetween {

    public FactionType factionType;
    public string displayname;
    public int maxPoints;
    public string[] modelHashes;

    public ArmyListGoBetween(ArmyList armyList)
    {
        factionType = armyList.faction.type;
        displayname = armyList.displayName;
        modelHashes = new string[armyList.pieces.Count];
        maxPoints = armyList.maxPoints;

        for (int i = 0; i < armyList.pieces.Count; i++) 
        {
            modelHashes[i] = armyList.pieces[i].hashID;
        }
    }

    public ArmyList ToArmyList()
    {
        var returnValue = new ArmyList
        {
            displayName = displayname,
            faction = Faction.FromType(factionType)
        };

        if (modelHashes.Length <= 0)
        {
            Debug.Log(displayname + " list empty");
        }
        var list = PieceList.allPieces.FindAll((obj) => obj.factionType == factionType);
        foreach (string hash in modelHashes)
        {
            PieceInfo info = list.Find((obj) => obj.hashID == hash);
            if (info == null)
                Debug.Log("Failed to load: " + hash);
            returnValue.pieces.Add(info);
            //returnValue.pieces.Add( list.Find((obj) => obj.hashID == hash) );
        }
        return returnValue;
    }
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}