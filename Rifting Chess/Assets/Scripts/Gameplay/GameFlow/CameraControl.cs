using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Camera camera1;
    public Camera camera2;

    public Camera activeCamera {
        get{
            return oneIsActive ? camera1 : camera2;
        }
    }
    public bool oneIsActive = true;

    public void SwitchCameras( int playerIndex ) {
        if (oneIsActive == (playerIndex == 1) )
            StartCoroutine(SwitchView());
    }

    IEnumerator SwitchView() {
        yield return new WaitForSeconds(0.5f);
        if (oneIsActive) {
            camera1.enabled = false;
            camera2.enabled = true;
            oneIsActive = false;
        } else {
            camera1.enabled = true;
            camera2.enabled = false;
            oneIsActive = true;
        }
    }
}
