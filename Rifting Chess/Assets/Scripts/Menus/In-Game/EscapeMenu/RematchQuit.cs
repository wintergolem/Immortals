using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RematchQuit : MonoBehaviour
{
    public void RematchButtonPressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.ForestMap);
    }

    public void QuitButtonPressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
