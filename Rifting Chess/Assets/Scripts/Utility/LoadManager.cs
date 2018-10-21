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
        new SceneInfo("Lobby" , "Scenes/Menus/GameLobby", SceneType.Menu)
    };
    public enum SceneList { MainMenu = 0,
        GameCreate =1 ,
        Prototype = 2 ,
        ForestMap = 3 ,
        Lobby = 4 };

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


}
