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
    public Dropdown listChoicePlayerOne;
    public Dropdown listChoicePlayerTwo;

	void Start () {
        //TODO: contact server about available games
        LoadManager.LoadAllLists();
        var lists = Account.instance.savedLists;
        var listsString = new List<string>();
        foreach (ArmyList list in lists)
        {
            listsString.Add(list.displayName);
        }
        listChoicePlayerOne.AddOptions(listsString);
        listChoicePlayerTwo.AddOptions(listsString);
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

    public void EditListButtonPressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.ArmyViewer);
    }

    public void CreateLocalPressed(){
        openLobbies.gameObject.SetActive(false);
        createNew.gameObject.SetActive(true);
    }

    public void BackButtonPressed()
    {
        openLobbies.gameObject.SetActive(true);
        createNew.gameObject.SetActive(false);
    }

    public void LocalGameStartPressed(){
        var sceneToLoad = LoadManager.SceneList.ForestMap;
        if (mapChoice.value != 0)
            sceneToLoad = LoadManager.SceneList.ForestMap;

        Account.instance.selectedList = Account.instance.savedLists[listChoicePlayerOne.value];
        Account.instance.opponentList = Account.instance.savedLists[listChoicePlayerTwo.value];
        LoadManager.LoadScene(sceneToLoad);
    }
}
