using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewFill : MonoBehaviour
{

    public GameObject optionPrefab;

    public int selectedValue = 0;
    ScrollViewOption selectedOption;

    public void AddOptions(List<ListSummary> lists)
    {
        for (int i = 0; i < lists.Count; i++)
        {
            var option = Instantiate(optionPrefab);
            option.GetComponent<ScrollViewOption>().AddInfo(lists[i], this, i);
            option.transform.SetParent(this.transform);
        }
    }

    public void AddOptions(List<string> maps)

    {
        for (int i = 0; i < maps.Count; i++)
        {
            var option = Instantiate(optionPrefab);
            option.GetComponent<ScrollViewOption>().AddInfo(maps[i], this, i);
            option.transform.SetParent(this.transform);
        }
    }

    public void ClaimSelected( ScrollViewOption option)
    {
        selectedOption.Unselect();
        selectedValue = option.value;
        selectedOption = option;
    }
}
