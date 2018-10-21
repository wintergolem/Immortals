using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServerContactManager {

    public static List<OpenLobbyInfo> GetOpenLobbies(){
        var list = new List<OpenLobbyInfo>();

        OpenLobbyInfo info = new OpenLobbyInfo();
        info.opponentName = "Blank";
        info.mapName = "Random";

        list.Add(info);
        return list;
    }
}
