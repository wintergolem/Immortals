using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentRunner : MonoBehaviour
{
    public static DeploymentRunner instance;
    public DeploySelectDisplay display;
    public List<Player> players;
    public List<List<GameObject>> pieceLists = new List<List<GameObject>>();
    public List<List<Square>> spawnPointsList = new List<List<Square>>();
    public int nextIndex;
    public int activePlayerIndex = 0;
    public bool rotateDeployment = false;
    public bool deployBaseChessGame = false;

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
        instance = this;

        if (deployBaseChessGame)
        {
            CalculateSpawnPoints();
            //spawn player 1's stuff
            activePlayerIndex = 0;
            var typed = pieceLists[0][0].GetComponent<Piece>().type;
            foreach (Square s in spawnPointsList[0])
            {
                GameObject piece = null;
                switch (s.baseSpawn)
                {
                    case "p":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("Pawn"));//grab piece to place
                        break;
                    case "r":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("Rook"));//grab piece to place
                        break;
                    case "b":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("Bishop"));//grab piece to place
                        break;
                    case "k":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("King"));//grab piece to place
                        break;
                    case "kn":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("Knight"));//grab piece to place
                        break;
                    case "q":
                        piece = pieceLists[0].Find(x => x.GetComponent<Piece>().displayName.Contains("Queen"));//grab piece to place
                        break;
                    default:
                        continue;
                }
                pieceLists[activePlayerIndex].Remove(piece);//remove piece from list of things to place
                GameManager.instance.boardLogic.AddPiece(piece, activePlayerIndex, s);//add piece to board
            }
            //spawn player 2's stuff
            activePlayerIndex = 1;
            foreach (Square s in spawnPointsList[1])
            {
                GameObject piece = null;
                switch (s.baseSpawn)
                {
                    case "p":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("Pawn"));//grab piece to place
                        break;
                    case "r":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("Rook"));//grab piece to place
                        break;
                    case "b":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("Bishop"));//grab piece to place
                        break;
                    case "k":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("King"));//grab piece to place
                        break;
                    case "kn":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("Knight"));//grab piece to place
                        break;
                    case "q":
                        piece = pieceLists[1].Find(x => x.GetComponent<Piece>().displayName.Contains("Queen"));//grab piece to place
                        break;
                    default:
                        continue;
                }
                pieceLists[activePlayerIndex].Remove(piece);//remove piece from list of things to place
                GameManager.instance.boardLogic.AddPiece(piece, activePlayerIndex, s);//add piece to board
            }
            Finished();
        }
        else
        {
            display.CreateButtons(pieceLists[0]);
        }
    }

    public static void PlaceNextPiece()
    {
        instance.PlaceNext();
    }

    void PlaceNext()
    {
        int targetSquareID = InputManager.lastSquareTouched;

        //verifiy the last grid point is valid
        Square square = GameManager.instance.boardLogic.map.SquareAt(targetSquareID);
        if (square.piece != null && !square.IsSpawnPoint(activePlayerIndex))
            return;
        if  (pieceLists[activePlayerIndex].Count != 0)
        {
            var piece = pieceLists[activePlayerIndex][nextIndex];//grab piece to place
            pieceLists[activePlayerIndex].Remove(piece);//remove piece from list of things to place
            GameManager.instance.boardLogic.AddPiece(piece, activePlayerIndex, square);//add piece to board
            HideSpawnOptions();

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

    void ShowSpawnOptions()
    {
        spawnPointsList[activePlayerIndex].ForEach(i => i.Highlight());
    }

    void HideSpawnOptions()
    {
        foreach(Square square in GameManager.instance.boardLogic.map.board)
        {
            square.UnHighlight();
        }
    }
    void CalculateSpawnPoints()
    {
        for(int i = 0; i<players.Count;i++)
        {
            spawnPointsList.Add(new List<Square>());
        }

        var board = GameManager.instance.boardLogic.map.board;
        foreach( Square square in board)
        {
            if( square.spawnValue != 0 && square.spawnValue <= spawnPointsList.Count) //spawnValue is not zero and is not greater than the # of players
            {
                spawnPointsList[square.spawnValue - 1].Add(square);
            }
        }
    }

    public void PieceSelected(int index)
    {
        nextIndex = index;
        if( spawnPointsList.Count == 0)
        {
            CalculateSpawnPoints();
        }
        ShowSpawnOptions();
    }
}
