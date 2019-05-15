using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyListOption : MonoBehaviour
{
    public ArmyList armyList;
    public Text text;

    public void Setup(ArmyList armyList)
    {
        text.text = armyList.displayName;
        this.armyList = armyList;
    }

    public void EditButtonPressed()
    {
        Account.instance.selectedList = armyList;
        LoadManager.LoadScene(LoadManager.SceneList.ArmyBuilder);
    }

    public void DeleteButtonPressed()
    {
        if (Account.instance.savedLists.Remove(armyList))
            LoadManager.SaveAllLists();
        else
            print("List failed to be removed");
        Destroy(gameObject);
    }
}
