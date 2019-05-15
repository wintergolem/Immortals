using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadManager {

    #region Scene Loading
    public static int mapCountMax = 10;
    public static SceneInfo[] SceneArray = new SceneInfo[]
    {
        new SceneInfo("MainMenu","Scenes/Menus/MainMenu", SceneType.Menu),
        new SceneInfo("GameCreate" , "Scenes/Menus/GameCreate", SceneType.Menu),
        new SceneInfo("Protoype" , "Scenes/Levels/Prototype", SceneType.Map),
        new SceneInfo("ForestMap" , "Scenes/Levels/NoBoardTest", SceneType.Map),
        new SceneInfo("ArmyBuild", "Scenes/Menus/ArmyBuilder", SceneType.Menu),
        new SceneInfo("ArmyViewer", "Scenes/Menus/ArmyListView", SceneType.Menu)
    };
    public enum SceneList
    {
        MainMenu = 0,
        Prototype = 2,
        ForestMap = 3,
        GameCreate = 1,
        ArmyBuilder = 4,
        ArmyViewer = 5
    };

    public static void LoadScene( SceneList selection ){
        LoadScene( SceneArray[ (int)selection ]);
    }
    public static void LoadScene( SceneInfo selection ){
        SceneManager.LoadScene( selection.location );
    }

    public static List<SceneInfo> GetMapList(){
        List<SceneInfo> returnList = new List<SceneInfo>();

        foreach (SceneInfo scene in SceneArray) {
            if (scene.type == SceneType.Map){
                returnList.Add(scene);
            }
        }
        return returnList;
    }
    #endregion

    #region In GameModel Storing

    public static List<List<GameObject>> pieceList = new List<List<GameObject>>();
    public static List<List<GameObject>> additionalPieces = new List<List<GameObject>>();

    public static void FillPrefabs(Player player, ArmyList list, bool useWhitePath)
    {
        while (player.playerNumber >= pieceList.Count)
        {
            pieceList.Add(new List<GameObject>());
        }
        GameObject prefab;
        foreach (PieceInfo p in list.pieces)
        {
            if (useWhitePath)
                 prefab =(GameObject)Resources.Load(p.whitePath);
            else
                prefab = (GameObject)Resources.Load(p.blackPath);

            prefab.GetComponent<Piece>().displayName = p.displayName;
            pieceList[player.playerNumber].Add(prefab);
        }

        foreach (List<GameObject> playerList in pieceList)
        {
            foreach (GameObject gameObject in playerList)
            {
                if (gameObject == null)
                {
                    Debug.Log("GameObject is null");
                }
            }
        }
    }

    public static int AddToAdditional(Player player, PieceInfo pieceInfo, bool useWhitePath)
    {
        while (player.playerNumber >= additionalPieces.Count)
        {
            additionalPieces.Add(new List<GameObject>());
        }

        if (useWhitePath)
            additionalPieces[player.playerNumber].Add((GameObject)Resources.Load(pieceInfo.whitePath));
        else
            additionalPieces[player.playerNumber].Add((GameObject)Resources.Load(pieceInfo.blackPath));

        return additionalPieces[player.playerNumber].Count - 1;
    }
    #endregion

    #region List saving and Loading

    static string dataPath;

    public static void SaveAllLists()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "ListData.txt");

        ArmyListGoBetween[] lists = new ArmyListGoBetween[Account.instance.savedLists.Count];
        for (int i = 0; i < Account.instance.savedLists.Count; i++)
        {
            lists[i] = new ArmyListGoBetween(Account.instance.savedLists[i]);
        }
        string jsonString = JsonHelper.ToJson(lists, true);
        File.WriteAllText(dataPath, jsonString);
    }

    public static void LoadAllLists()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "ListData.txt");
        if (File.Exists(dataPath))
        {
            string dataAsJson = File.ReadAllText(dataPath);
            var rawData = JsonHelper.FromJson<ArmyListGoBetween>(dataAsJson);
            var lists = new List<ArmyList>();
            foreach (ArmyListGoBetween goBetween in rawData)
            {
                lists.Add(goBetween.ToArmyList());
            }
            Account.instance.savedLists = lists;
            //using (StreamReader streamReader = File.OpenText(dataPath))
            // {
            //string jsonString = streamReader.ReadToEnd();

            //var rawData = JsonUtility.FromJson<List<ArmyListGoBetween>>(jsonString);
            //var rawData = JsonHelper.FromJson<ArmyListGoBetween>(jsonString);

            /*var returnValue = new List<ArmyList>();
                foreach (ArmyListGoBetween goBetween in rawData)
                {
                    returnValue.Add(goBetween.ToArmyList());
                }*/

            //Account.instance.savedLists = returnValue;
            //}
        }
        else
        {
            Debug.Log("File doesn't exist" + dataPath);
        }
    }

    #endregion
}
