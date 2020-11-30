using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionOption : MonoBehaviour
{
    public delegate void OnClickDelegate(FactionType type);

    public Text optionText; //set in editor
    public Image optionImage; //set in editor
    public Button button; // set in editor

    FactionType faction = FactionType.Phalanx;
    Sprite factionImage;

   public void Init(FactionType aFaction , OnClickDelegate onClickDelegate , Sprite image = null)
    {
        faction = aFaction;
        optionText.text = faction.ToString();
        if( image != null)
        {
            factionImage = image;
            optionImage.sprite = factionImage;
        }
        button.onClick.AddListener(() => { onClickDelegate(faction); });
    }

    public void buttonTest()
    {
        Debug.Log("Button pressed");
    }
}
