using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromistress : Queen {

    int pieceToSummon;

    int pawnToSummon;
    int rookToSummon;
    int knightToSummon;
    int bishopToSummon;
    int queenToSummon;

    public Necromistress(){
        enemyCaptured = CheckQueenCaptured;
    }

    public override void Awake()
    {
        base.Awake();

        pawnToSummon = LoadManager.AddToAdditional(player, PieceList.allPieces.Find((obj) => obj.displayName == "Zombie"), (player.forward > 0));
        rookToSummon = LoadManager.AddToAdditional(player, PieceList.allPieces.Find((obj) => obj.displayName == "Undead Rook"), (player.forward > 0));
        knightToSummon = LoadManager.AddToAdditional(player, PieceList.allPieces.Find((obj) => obj.displayName == "Undead Knight"), (player.forward > 0));
        bishopToSummon = LoadManager.AddToAdditional(player, PieceList.allPieces.Find((obj) => obj.displayName == "Unholy Bishop"), (player.forward > 0));
        queenToSummon = LoadManager.AddToAdditional(player, PieceList.allPieces.Find((obj) => obj.displayName == "Necromistress"), (player.forward > 0));
    }

    public void CheckQueenCaptured()
    {
        if (GameManager.instance.pieceCapturing == this) {
            GameLog.AddText(displayName + "''s Tortured Servitude Triggered");
            GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Add(SummonCapturedPiece);

           switch (GameManager.instance.pieceBeingCaptured.type)
            {
                case PieceType.Pawn:
                    pieceToSummon = pawnToSummon;
                    break;
                case PieceType.Rook:
                    pieceToSummon = rookToSummon;
                    break;
                case PieceType.Knight:
                    pieceToSummon = knightToSummon;
                    break;
                case PieceType.Bishop:
                    pieceToSummon = bishopToSummon;
                    break;
                case PieceType.Queen:
                    pieceToSummon = queenToSummon;
                    break;
            }
        } 
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
    }

    public void CancelSummon(){
        GameLog.AddText(displayName + "''s Tortured Servitude Cancelled");
        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(SummonCapturedPieceFinish);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
    }

    public void SummonCapturedPiece(){
        var manager = GameManager.instance;
        King king = (King)manager.activePlayer.GetKing();
        if (manager.boardLogic.MovesForPiece(king).Count != 0)
        {
            GameLog.AddText(displayName + "''s Tortured Servitude Active");
            manager.boardLogic.SelectPiece(king);
            GameNoticationCenter.instance.ClickedSquare.Add(SummonCapturedPieceFinish);
            GameNoticationCenter.instance.RightClick.Add(CancelSummon);
        }
        else
        {
            GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
            GameLog.AddText(displayName + "''s Tortured Servitude Cancelled due to no valid squares available");
        }

        GameManager.instance.players[playerIndex].noticationCenter.TurnStart.Remove(SummonCapturedPiece);
    }

    public void SummonCapturedPieceFinish(){
        GameLog.AddText(displayName + "''s Tortured Servitude Finished");
        var gridPoint = InputManager.lastGridPoint;
        GameManager.instance.PlacePiece(LoadManager.additionalPieces[playerIndex][pieceToSummon], gridPoint);
        GameManager.instance.moveTaken = true;
        GameNoticationCenter.instance.ClickedSquare.Remove(SummonCapturedPieceFinish);
        GameNoticationCenter.instance.RightClick.Remove(CancelSummon);
        GameManager.instance.players[playerIndex].noticationCenter.runner.RunNext();
        GameNoticationCenter.instance.runner.RunNext();
    }
}
