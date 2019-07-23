using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    enum MainMenuState { Entry, Nav };
    MainMenuState state = MainMenuState.Entry;

    public GameObject entryUI;
    public GameObject navUI;

    void Start()
    {
        entryUI.SetActive(true);
        navUI.SetActive(false);
    }

    void Update()
    {
        if (state == MainMenuState.Entry && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0)))
        {
            ChangeStateToNav();
        }
    }

    void ChangeStateToNav()
    {
        state = MainMenuState.Nav;
        entryUI.SetActive(false);
        navUI.SetActive(true);
    }

    public void BuildListPressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.ArmyViewer);
    }

    public void PlayGamePressed()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }

    public void ArmyGalleryPressed()
    {
         
    }
}
