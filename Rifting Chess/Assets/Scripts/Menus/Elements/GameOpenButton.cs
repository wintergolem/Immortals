using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOpenButton : MonoBehaviour {

    public Text opponentText;
    public Text mapNameText;

    public OpenLobbyInfo gameInfo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetInfo( OpenLobbyInfo info ){
        gameInfo = info;
        opponentText.text = gameInfo.opponentName;
        mapNameText.text = gameInfo.mapName;
    }
}
