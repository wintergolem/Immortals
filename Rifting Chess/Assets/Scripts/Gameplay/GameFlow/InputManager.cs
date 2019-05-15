using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class InputManager : MonoBehaviour {

    public Button powerButton;
    public Text buttonText;
    public GameObject escapeMenu;

    private bool mouseOverLastUpdate = false;
    private bool appHasFocus = true;
    private bool cursorLocked = false;

    public static Vector2Int lastGridPoint = new Vector2Int();

    private void Start()
    {
        powerButton.onClick.AddListener(GameManager.instance.PowerButtonPressed);
        buttonText = powerButton.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (!appHasFocus)
        {
            return;
        }
        if (!cursorLocked)
        {
            if (Input.GetMouseButton(0))
            {
                Cursor.lockState = CursorLockMode.Confined;
                cursorLocked = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLocked = false;
            escapeMenu.SetActive(true);
            return;
        }

        Ray ray = GameManager.instance.cameraControl.activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            var point = hit.point;
            lastGridPoint = Geometry.GridFromPoint(point);

            //send position to gameManager
            GameNoticationCenter.TriggerEvent(GameEventTrigger.HoverSquare);   //GameManager.instance.MouseOver(gridPoint);
            mouseOverLastUpdate = true;

            if (Input.GetMouseButtonDown(0)) {
                if (!GameManager.instance.deploymentDone)
                {
                    DeploymentRunner.PlaceNextPiece();
                }
                else if (!GameManager.instance.boardLogic.map.SquareAt(lastGridPoint).Empty)
                    GameNoticationCenter.TriggerEvent(GameEventTrigger.ClickedOnPiece);
                else
                    GameNoticationCenter.TriggerEvent(GameEventTrigger.ClickedOnSquare);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GameNoticationCenter.TriggerEvent(GameEventTrigger.RightClick);
            }
        }
        else {
            //send <no hit> to gamemanager
            if (mouseOverLastUpdate) {
                GameNoticationCenter.TriggerEvent(GameEventTrigger.RemoveHover); //GameManager.instance.RemoveMouseOver();
                mouseOverLastUpdate = false;
            }
        }
    } //end of update()

    public void ChangeButtonText(string text) {
        buttonText.text = text;
    }

    public void ButtonVisable(bool isVisible) {
        powerButton.gameObject.SetActive(isVisible);
    }

    public void OnApplicationFocus(bool focus)
    {
        appHasFocus = focus;
    }

} //end of class
