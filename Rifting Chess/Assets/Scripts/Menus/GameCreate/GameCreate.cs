using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCreate : MonoBehaviour {

    public GameObject gameListButton;
    public GameObject content;
    public Canvas openLobbies;
    public Canvas createNew;

    public ScrollViewFill playerOneList;
    public ScrollViewFill playerTwoList;
    public ScrollViewFill mapSelect;

	void Start () {
        //TODO: contact server about available games
        LoadManager.LoadAllLists();
        var lists = Account.instance.savedLists;
        var armyListStringArray = new List<ListSummary>();
        foreach (ArmyList list in lists)
        {
            armyListStringArray.Add( new ListSummary( list.displayName, list.faction.ToString() ));
        }
        playerOneList.AddOptions(armyListStringArray);
        playerTwoList.AddOptions(armyListStringArray);

        List<string> mapListStringArray = new List<string>
        {
            LoadManager.SceneList.ForestMap.ToString()
        };
        mapSelect.AddOptions(mapListStringArray);

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
        var selectedMap = new LoadManager.SceneList();
        switch (mapSelect.selectedValue)
        {
            case 0:
                selectedMap = LoadManager.SceneList.ForestMap;
                break;
            default:
                selectedMap = LoadManager.SceneList.ForestMap;
                break;
        }
        //TODO: add protection here
        Account.instance.selectedList = Account.instance.savedLists[playerOneList.selectedValue];
        Account.instance.opponentList = Account.instance.savedLists[playerTwoList.selectedValue];

        LoadManager.LoadScene(selectedMap);
    }
}

public struct ListSummary
{
    public string listName;
    public string listFaction;

    public ListSummary(string displayName, string factionName) : this()
    {
        this.listName = displayName;
        this.listFaction = factionName;
    }
}