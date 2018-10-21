using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    public Button powerButton;
    public Text buttonText;
    private bool mouseOverLastUpdate = false;

    private void Start()
    {
        powerButton.onClick.AddListener(GameManager.instance.PowerButtonPressed);
        buttonText = powerButton.GetComponentInChildren<Text>();
    }

    void Update()
    {
        Ray ray = GameManager.instance.cameraControl.activeCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin,ray.direction*10,Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            //send position to gameManager
            GameManager.instance.MouseOver(gridPoint);
            mouseOverLastUpdate = true;

            if (Input.GetMouseButtonDown(0)) {
                //send mouseDown(0) to gameManager
                GameManager.instance.LeftMouseClick(point);
            } else if (Input.GetMouseButtonDown(1)) {
                //send mouseDown(1) to gameManager
                GameManager.instance.RightMouseClick(point);
            }
          }
        else {
            //send <no hit> to gamemanager
            if (mouseOverLastUpdate) {
                GameManager.instance.RemoveMouseOver();
                mouseOverLastUpdate = false;
            }
        }
    } //end of update()

    public void ChangeButtonText(string text){
        buttonText.text = text;
    }

    public void ButtonVisable( bool isVisible){
        powerButton.gameObject.SetActive(isVisible);
    }
} //end of class
