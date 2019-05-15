using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployPieceButton : MonoBehaviour
{
    public Text text;

    public void Init(string displayString, int indexToSend)
    {
        text.text = displayString;
        GetComponent<Button>().onClick.AddListener(delegate { DeploymentRunner.instance.display.ButtonPressed(indexToSend); });
    }
}
