using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI {

    BoardLogic boardLogic;
    Player playerControlled;

    List<AIMove> movesPossible;
    int totalWeight = 0;

    public BasicAI( BoardLogic logic , Player player) {
        boardLogic = logic;
        playerControlled = player;
        movesPossible = new List<AIMove>();
    }

    #region Decision making functions
    public IEnumerator DecideMove(){
        yield return new WaitForSeconds(1f);

        totalWeight = 0;
        GenerateMoves();
        //TODO:
        AIMove choosenMove = SelectMove();
        boardLogic.movingPiece = choosenMove.piece;
        boardLogic.moveLocations.Clear();
        boardLogic.moveLocations.Add(choosenMove.destination);
        GameManager.instance.CheckMove( choosenMove.destination, boardLogic.PieceAtGrid(choosenMove.destination));
    }

    public void ChoosePawnSummon(){
        Vector2Int choosen = boardLogic.moveLocations[Random.Range(0,boardLogic.moveLocations.Count)];
        GameManager.instance.PlacePiece(LoadManager.blackPawn , choosen);
    }
    #endregion

    #region Helper functions

    void GenerateMoves(){
        movesPossible.Clear();
        totalWeight = 0;
        foreach (Piece p in playerControlled.pieces) {
            List<AIMove> moves = boardLogic.MovesForAIPiece(p);
            movesPossible.AddRange(moves);
        }

        foreach (AIMove a in movesPossible){
            a.weight = DetermineWeight(a);
        }
    }

    int DetermineWeight(AIMove move){
        int returnValue = 0;
        Square space = boardLogic.map.SquareAt(move.destination);
        Piece piece = space.piece;//boardLogic.PieceAtGrid(move.destination);
        if (piece != null){
            returnValue += 10;
            if (piece.value > move.piece.value){
                returnValue += (piece.value - move.piece.value) * 10;
            }
        } else if (space.Threatened(playerControlled.playerNumber) ) {
            returnValue += 1;
        }else {
            returnValue += 5;
        }
        //Debug.Log("wieght for " + move.piece.name + " : " + returnValue.ToString() );  
        totalWeight += returnValue;
        return returnValue;
    }

    AIMove SelectMove(){
        int random = Random.Range(0, totalWeight);
        foreach (AIMove move in movesPossible){
            if (random <= move.weight){
                return move;
            } else {
                random -= move.weight;
            }
        }
        Debug.LogAssertion("Basic AI - SelectMove: random > totatlWeight");
        return null;
    }
    #endregion
}
