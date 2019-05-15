using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeQuit : MonoBehaviour
{
    public GameObject escapeMenu;

    public void ResumeButtonPressed()
    {
        escapeMenu.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
