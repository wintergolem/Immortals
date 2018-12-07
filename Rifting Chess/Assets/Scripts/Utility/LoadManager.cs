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
        new SceneInfo("Lobby" , "Scenes/Menus/GameLobby", SceneType.Menu),
        new SceneInfo("ArmyBuild", "Scenes/Menus/ArmyBuilder", SceneType.Menu)
    };
    public enum SceneList { MainMenu = 0,
        GameCreate =1 ,
        Prototype = 2 ,
        ForestMap = 3 ,
        Lobby = 4 ,
        ArmyBuilder = 5};

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

    #region Model Storing

    public static GameObject blackPawn;
    public static GameObject blackRook;
    public static GameObject blackKnight;
    public static GameObject blackBishop;
    public static GameObject blackQueen;
    public static GameObject blackKing;
    public static GameObject whitePawn;
    public static GameObject whiteRook;
    public static GameObject whiteKnight;
    public static GameObject whiteBishop;
    public static GameObject whiteQueen;
    public static GameObject whiteKing;

    public static void FillPrefabs( Player player , ArmyList list) {
        if (player.playerNumber == 0){
            whitePawn = (GameObject)Resources.Load(list.pawn.whitePath);
            whiteRook = (GameObject)Resources.Load(list.rook.whitePath);
            whiteKnight = (GameObject)Resources.Load(list.knight.whitePath);
            whiteBishop = (GameObject)Resources.Load(list.bishop.whitePath);
            whiteQueen = (GameObject)Resources.Load(list.queen.whitePath);
            whiteKing = (GameObject)Resources.Load(list.king.whitePath);
        } else {
            blackPawn = (GameObject)Resources.Load(list.pawn.blackPath);
            blackRook = (GameObject)Resources.Load(list.rook.blackPath);
            blackKnight = (GameObject)Resources.Load(list.knight.blackPath);
            blackBishop = (GameObject)Resources.Load(list.bishop.blackPath);
            blackQueen = (GameObject)Resources.Load(list.queen.blackPath);
            blackKing = (GameObject)Resources.Load(list.king.blackPath);
        }

    }
    #endregion
}
