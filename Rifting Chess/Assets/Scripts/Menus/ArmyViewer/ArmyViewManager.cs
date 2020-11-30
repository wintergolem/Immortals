using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyViewManager : MonoBehaviour
{

    public GameObject ArmyViewOption;
    public GameObject viewContent;

    public GameObject factionSelector;
    public GameObject factionSelectorContent; //set in editor
    public GameObject FactionSelectOption;


    bool factionSelectionCreated = false;
    void Start()
    {

        LoadManager.LoadAllLists();
        var lists = Account.instance.savedLists;

        foreach (ArmyList list in lists)
        {
            var button = Instantiate(ArmyViewOption);
            button.GetComponent<ArmyListOption>().Setup(list);
            button.transform.SetParent(viewContent.transform);
        }
    }

    public void ShowFactionSelector()
    {
        if( !factionSelectionCreated )
        {
            FactionType[] factionList = FactionTypeFunc.GetArray();
            foreach( FactionType faction in factionList)
            {
                var option = Instantiate(FactionSelectOption);
                option.GetComponent<FactionOption>().Init(faction , CreateNewArmy);
                option.transform.SetParent(factionSelectorContent.transform);
            }

            factionSelectionCreated = true;
        }

        factionSelector.SetActive(true);
    }

    public void HideFactionSelector()
    {
        factionSelector.SetActive(false);
    }

    public void CreateNewArmy( FactionType factionSelected)
    {
        ArmyList armyList = new ArmyList();
        armyList.faction = factionSelected;
        Account.instance.selectedList = armyList;
        LoadManager.LoadScene(LoadManager.SceneList.ArmyBuilder);
    }

    public void BackButton()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
