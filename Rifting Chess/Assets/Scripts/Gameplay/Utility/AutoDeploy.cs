using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeploy : MonoBehaviour
{
    public DeploySelectDisplay display;
    public DeploymentRunner runner;

    public Vector2Int deployAt = new Vector2Int(-1, 0);
    int playerOneSecondRow = 0;
    int playerTwoSecondRow = 0;

    void Update()
    {
        if (!GameManager.instance.deploymentDone)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                display.ButtonPressed(0);
                InputManager.lastGridPoint = GetNextLocation();
                DeploymentRunner.PlaceNextPiece();
            }
        }
        else
        {
            Destroy(this);
        }
    }

    Vector2Int GetNextLocation()
    {
        deployAt = new Vector2Int(deployAt.x + 1, deployAt.y);
        if (deployAt.x >= GameManager.instance.boardLogic.map.squares.GetLength(0))
        {
            deployAt.x = 0;
            deployAt.y = runner.activePlayerIndex == 0 ? 0 + playerOneSecondRow : 7 - playerTwoSecondRow;
            if (runner.activePlayerIndex == 0)
                playerOneSecondRow = 1;
            else
                playerTwoSecondRow = 1;
        }
        return deployAt;
    }
}
