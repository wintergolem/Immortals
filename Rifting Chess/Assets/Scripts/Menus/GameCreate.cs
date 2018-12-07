using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCreate : MonoBehaviour {

    public GameObject gameListButton;
    public GameObject content;
    public Canvas openLobbies;
    public Canvas createNew;
    public Dropdown mapChoice;

	void Start () {
        //TODO: contact server about available games
        CreateButtons();
    }
	
	void Update () {	}

    void CreateButtons() {
        var lobbies = ServerContactManager.GetOpenLobbies();

        foreach (OpenLobbyInfo lobby in lobbies){
            var button = Instantiate(gameListButton);
            button.GetComponent<GameOpenButton>().GetInfo(lobby);
            button.transform.SetParent(content.transform);
        }
    }

    public void CreateAIPressed(){
        openLobbies.gameObject.SetActive(false);
        createNew.gameObject.SetActive(true);
    }

    public void AiGameStartPressed(){
        var sceneToLoad = LoadManager.SceneList.Prototype;
        if (mapChoice.value != 0)
            sceneToLoad = LoadManager.SceneList.ForestMap;
        LoadManager.LoadScene(sceneToLoad);
    }
}
