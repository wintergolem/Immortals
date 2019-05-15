using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.anyKeyDown){
            LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
        }
	}
}
