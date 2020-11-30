using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeploy : MonoBehaviour
{
    public DeploySelectDisplay display;
    public DeploymentRunner runner;

    public int deployAt = 0; //squareID

    void Update()
    {
        if (!GameManager.instance.deploymentDone)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                display.ButtonPressed(0);
                InputManager.lastSquareTouched = GetNextLocation();
                DeploymentRunner.PlaceNextPiece();
            }
        }
        else
        {
            Destroy(this);
        }
    }

    int GetNextLocation()
    {
        Square[] squares = GameManager.instance.boardLogic.map.board;
        int activePlayer = GameManager.instance.deploymentRunner.activePlayerIndex;
        foreach (Square square in squares)
        {
            if (square.IsSpawnPoint(activePlayer) && square.IsEmpty)
            {
                return square.UniqueID;
            }
        }
        Debug.LogError("Unable to find place to spawn piece for player " + GameManager.instance.deploymentRunner.activePlayerIndex);
        return 0;
    }
}
