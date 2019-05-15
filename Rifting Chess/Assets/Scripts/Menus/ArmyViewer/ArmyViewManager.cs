using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyViewManager : MonoBehaviour
{

    public GameObject ArmyViewOption;
    public GameObject viewContent;

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

    public void CreateNewArmy()
    {
        Account.instance.selectedList = null;
        LoadManager.LoadScene(LoadManager.SceneList.ArmyBuilder);
    }

    public void BackButton()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
