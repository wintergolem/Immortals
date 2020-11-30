using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewOption : MonoBehaviour
{
    public Text listName;
    public Text listFaction;
    public int value;
    public Toggle toggle;
    ScrollViewFill owner;

    public void AddInfo(ListSummary summary, ScrollViewFill owner, int placeInList)
    {
        listName.text = summary.listName;
        listFaction.text = summary.listFaction;
        this.owner = owner;
        value = placeInList;
    }

    public void AddInfo(string mapName, ScrollViewFill owner, int placeInList)
    {
        listName.text = mapName;
        this.owner = owner;
        value = placeInList;
    }

    public void Selected()
    {
        owner.selectedValue = value;
    }

    public void Unselect()
    {
        toggle.isOn = false;
    }
}
