using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    public static GameLog instance;

    public Text text;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public static void AddText(string newText)
    {
        instance.text.text += newText + "\n";
    }
}
