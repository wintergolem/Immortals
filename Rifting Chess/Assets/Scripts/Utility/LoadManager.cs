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
        new SceneInfo("ArmyViewer", "Scenes/Menus/ArmyListView", SceneType.Menu),
        new SceneInfo("ChessMap", "Scenes/Levels/Prototype", SceneType.Map)
    };
    public enum SceneList
    {
        MainMenu = 0,
        Prototype = 2,
        ForestMap = 3,
        GameCreate = 1,
        ArmyBuilder = 4,
        ArmyViewer = 5,
        ChessMap = 6
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

    #region In Game Model Storing

    public static List<List<GameObject>> pieceList = new List<List<GameObject>>();
    public static List<List<GameObject>> additionalPieces = new List<List<GameObject>>();

    public static void FillPrefabs(Player player, ArmyList list)
    {
        while (player.playerNumber >= pieceList.Count)
        {
            pieceList.Add(new List<GameObject>());
        }
        GameObject prefab;
        foreach (PieceInfo p in list.pieces)
        {
            prefab = (GameObject)Resources.Load(p.modelPath);

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

    public static int AddToAdditional(Player player, PieceInfo pieceInfo)
    {
        while (player.playerNumber >= additionalPieces.Count)
        {
            additionalPieces.Add(new List<GameObject>());
        }
        additionalPieces[player.playerNumber].Add((GameObject)Resources.Load(pieceInfo.modelPath));
       

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
        var lists = new List<ArmyList>();
        if (File.Exists(dataPath))
        {
            string dataAsJson = File.ReadAllText(dataPath);
            var rawData = JsonHelper.FromJson<ArmyListGoBetween>(dataAsJson);
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
        else //file doesn't exist so make a list and save it to create the file
        {
            Debug.Log("File doesn't exist" + dataPath);

            /*
             * Create basic armyList
             * Add armyList to Account.instance.savedLists
             * Call SaveAllLists
             */

            ArmyList basicZomArmy = new ArmyList
            {
                faction = FactionType.Undead,
                displayName = "Basic Undead Army"
            };
            List<PieceInfo> allpieces = new List<PieceInfo>(PieceList.allPieces);
            for( int i = 0; i<9; i++) basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Zombie"));
            for (int i = 0; i < 2; i++) basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Unholy Bishop"));
            for (int i = 0; i < 2; i++) basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Undead Rook"));
            for (int i = 0; i < 2; i++) basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Undead Knight"));
            basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Necromistress"));
            basicZomArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Arch-Necromancer"));

            Account.instance.savedLists.Add(basicZomArmy);
            lists.Add(basicZomArmy);

            ArmyList basicArmy = new ArmyList
            {
                faction = FactionType.Base,
                displayName = "Basic Chess Army"
            };
            for (int i = 0; i < 9; i++) basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Pawn"));
            for (int i = 0; i < 2; i++) basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Bishop"));
            for (int i = 0; i < 2; i++) basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Rook"));
            for (int i = 0; i < 2; i++) basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Knight"));
            basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "Queen"));
            basicArmy.pieces.Add(allpieces.Find((obj) => obj.displayName == "King"));

            Account.instance.savedLists.Add(basicArmy);
            lists.Add(basicArmy);
            SaveAllLists();

        }

        //assign lists to serve as default for debugging
        Account.AssignList(lists[1], 0);
        Account.AssignList(lists[1], 1);
    }

    #endregion

    #region indicators loading

    static string indicatorsPath = "Assets/Resources/Prefabs/Selection";
    static IndicatorBase[] threatIndicators;
    static IndicatorBase[] moveIndicators;
    public static IndicatorBase[] GetAllThreatIndicators()
    {
        if ( threatIndicators == null)
        {
            threatIndicators = Resources.LoadAll(indicatorsPath + "/ThreatIndicators") as IndicatorBase[];
            Account.instance.RunUnlockCheckOnIndicators(ref threatIndicators);
        }
        return threatIndicators;
    }

    public static IndicatorBase[] GetAllMovendicators()
    {
        if (moveIndicators == null)
        {
            moveIndicators = Resources.LoadAll(indicatorsPath + "/MoveIndicators") as IndicatorBase[];
            Account.instance.RunUnlockCheckOnIndicators(ref moveIndicators, false);
        }
        return moveIndicators;
    }

    #endregion
}
