using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentRunner : MonoBehaviour
{
    public static DeploymentRunner instance;
    public DeploySelectDisplay display;
    public List<Player> players;
    public List<List<GameObject>> pieceLists = new List<List<GameObject>>();
    public int nextIndex;
    public int activePlayerIndex = 0;
    public bool rotateDeployment = false;

    Player ActivePlayer  {
        get
        {
            return players[activePlayerIndex];
        }
    }

    public void Startup()
    {
        players = GameManager.instance.players;
        pieceLists.Add( LoadManager.pieceList[0]);
        pieceLists.Add(LoadManager.pieceList[1]);

        display.CreateButtons(pieceLists[0]);
        instance = this;
    }

    public static void PlaceNextPiece()
    {
        instance.PlaceNext();
    }

    void PlaceNext()
    {
        Vector2Int target = InputManager.lastGridPoint;

        //verifiy the last grid point is valid
        Square square = GameManager.instance.boardLogic.map.SquareAt(target);
        if (square.piece != null)
            return;
        if ((activePlayerIndex == 0 && target.y > 1) || (activePlayerIndex == 1 && target.y < 6))
            return;
        if  (pieceLists[activePlayerIndex].Count != 0)//(pieceIndices[activePlayerIndex ] < LoadManager.pieceList[activePlayerIndex].Count)
        {
            var piece = pieceLists[activePlayerIndex][nextIndex];//LoadManager.pieceList[activePlayerIndex][pieceIndices[activePlayerIndex]];
            pieceLists[activePlayerIndex].Remove(piece);
            GameManager.instance.boardLogic.AddPiece(piece, activePlayerIndex, target.x, target.y);

            if (rotateDeployment || pieceLists[activePlayerIndex].Count == 0 && activePlayerIndex != 1)
            {
                AdvancePlayerIndex();
                GameManager.instance.cameraControl.SwitchCameras(activePlayerIndex);
                display.dialog.SetActive(true);
                display.CreateButtons(pieceLists[activePlayerIndex]);
            }
            else
            {
                display.dialog.SetActive(true);
                display.CreateButtons(pieceLists[activePlayerIndex]);
            }
        }
       
       if (pieceLists[0].Count == 0 && pieceLists[1].Count == 0)
            {
                Finished();
                return;
            }
    }

    public void Finished()
    {
       if (GameManager.instance.deploymentDone)
           return;
        GameManager.instance.deploymentDone = true;
        display.dialog.SetActive(false);
        this.enabled = false;
        GameManager.instance.PiecesAdded();
        Destroy(display.dialog.gameObject);
        Destroy(this);
    }

    static int loopCount = 0;
    void AdvancePlayerIndex()
    {
        activePlayerIndex += 1;
        if (activePlayerIndex >= players.Count)
            activePlayerIndex = 0;

        if (pieceLists[activePlayerIndex].Count == 0)
        {
            loopCount += 1;
            if (loopCount > pieceLists.Count)
            {
                Finished();
                return;
            }
            AdvancePlayerIndex();
        }

        loopCount = 0;
    }
}
