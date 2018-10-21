using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLobbyInfo{
    public string opponentName;
    public string mapName;

}

public enum SceneType {Menu, Map};
public class SceneInfo{
    public string name;
    public string location;
    public SceneType type;

    public SceneInfo( string a_name, string a_location, SceneType a_type){
        name = a_name;
        location = a_location;
        type = a_type;
    }
}