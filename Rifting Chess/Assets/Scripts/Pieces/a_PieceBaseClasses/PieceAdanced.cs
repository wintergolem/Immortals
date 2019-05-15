﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceAdanced : Piece
{

    //properties for all pieces
    protected int ForwardDirection
    {
        get
        {
            return player.forward;
        }
    }

    protected bool specialMovementUsed = false;

    #region movement and threat
    protected void MoveAlongAxis(int index, bool canJump)
    {
        Square check = square;
        int currentMoveJumpCon = 0;
        int currentSquaresMoved = 0;
        do
        {
            Square next = check.neighbors[index];
            check = next;
            currentSquaresMoved++;

            if (next == null || currentSquaresMoved >= maxSquaresCanMove)
                break;
            if (next.piece != null)
            {
                if (canJump && (currentMoveJumpCon < maxMovePieceJumpsCon))
                {
                    currentMoveJumpCon++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            moveLocations.Add(next.personalCoord);
        } while (check.Empty);
    }

    protected void ThreatAlongAxis(int index, bool canJump)
    {
        Square check = square;
        int currentAttackJumpCon = 0;
        int currentSquaresMoved = 0;
        do
        {
            Square next = check.neighbors[index];
            check = next;
            currentSquaresMoved++;

            if (next == null || currentSquaresMoved > maxSquaresCanMove)
                break;
            if (next.piece == null)
                continue;

            if (next.piece.playerIndex != playerIndex)
            {
                ThreatInTheory.Add(next.personalCoord);
                ThreatWithPieces.Add(next.personalCoord);
            }

            if (canJump && currentAttackJumpCon < maxCapturePieceJumpsCon)
            {
                currentAttackJumpCon++;
            }
            else
            {
                break;
            }
        } while (check.Empty);
    }

    protected void MoveAlongL()
    {
        List<int> moveDirections = new List<int>(RookIndexes);
        Vector2Int[] checkDirections = new Vector2Int[8];
        checkDirections[0] = new Vector2Int(7, 1);
        checkDirections[2] = new Vector2Int(1, 3);
        checkDirections[4] = new Vector2Int(3, 5);
        checkDirections[6] = new Vector2Int(7, 5);

        foreach (int index in moveDirections)
        {
            Square check = square.neighbors[index];
            if (check != null)
            {
                Vector2Int coord = checkDirections[index];
                Square temp = check.neighbors[coord.x];
                if (temp != null && (temp.Empty || temp.piece.playerIndex != playerIndex))
                {
                    moveLocations.Add(temp.personalCoord);
                }
                temp = check.neighbors[coord.y];
                if (temp != null && (temp.Empty || temp.piece.playerIndex != playerIndex))
                {
                    moveLocations.Add(temp.personalCoord);
                }
            }
        }
    }

    protected void ThreatAlongL()
    {
        List<int> moveDirections = new List<int>(RookIndexes);
        Vector2Int[] checkDirections = new Vector2Int[8];
        checkDirections[0] = new Vector2Int(7, 1);
        checkDirections[2] = new Vector2Int(1, 3);
        checkDirections[4] = new Vector2Int(3, 5);
        checkDirections[6] = new Vector2Int(7, 5);

        foreach (int index in moveDirections)
        {
            Square check = square.neighbors[index];
            if (check != null)
            {
                Vector2Int coord = checkDirections[index];
                Square temp = check.neighbors[coord.x];
                if (temp != null)
                {
                    ThreatInTheory.Add(temp.personalCoord);
                    if (!temp.Empty && temp.piece.playerIndex != playerIndex)
                    {
                        ThreatWithPieces.Add(temp.personalCoord);
                    }
                }
                temp = check.neighbors[coord.y];
                if (temp != null)
                {
                    ThreatInTheory.Add(temp.personalCoord);
                    if (!temp.Empty && temp.piece.playerIndex != playerIndex)
                    {
                        ThreatWithPieces.Add(temp.personalCoord);
                    }
                }
            }
        }
    }
    #endregion

    #region ShieldGuard
    public void ShieldGuard(Piece piece)
    {
        square.piece = null;
        piece.MoveTo(square.personalCoord, false);

        OnCaptured();
    }
    #endregion
    #region berserk
    protected bool berserking = false;
    protected int maxBerserkInstance = 1;
    int currentBerserkInstance = 0;
    protected int maxBerserkCons = 1;
    int currentBerserkCons = 0;

    public virtual void Berserk()
    {
        if (GameManager.instance.pieceCapturing != this || (currentBerserkInstance >= maxBerserkInstance))
        {
            berserking = false;
        }
        else
        {
            player.noticationCenter.TurnEnd.Add(BerserkDisplay);
            player.noticationCenter.EnemyCaptured.Remove(Berserk);
            GameManager.instance.stuffTakingFocus.Add(this);
        }
        player.noticationCenter.runner.RunNext();
    }

    public virtual void BerserkDisplay()
    {
        List<Vector2Int> threat = GetThreatLocations(true);
        if (threat.Count > 0)
        {
            berserking = true;
            Select();
            RemoveHighlight();
            GameManager.instance.boardLogic.HighlightSquares(threat, false);
            highlightLocations.Clear();
            highlightLocations.AddRange(threat);

            //add actions
            GameNoticationCenter.instance.ClickedPiece.Insert(0, BerserkTargetSelected);
            GameNoticationCenter.instance.RightClick.Add(CancelBerserk);
        }
        else
        {
            GameLog.AddText("Berserk Cancelled due to no targets");
            player.noticationCenter.EnemyCaptured.Add(Berserk);
            UnSelect();
            GameManager.instance.stuffTakingFocus.Remove(this);
        }
        player.noticationCenter.TurnEnd.Remove(BerserkDisplay);
        player.noticationCenter.runner.RunNext();
    }

    public void CancelBerserk()
    {
        GameLog.AddText("Berserk cancelled");
        GameNoticationCenter.instance.ClickedPiece.Remove(BerserkTargetSelected);
        GameNoticationCenter.instance.RightClick.Remove(CancelBerserk);
        player.noticationCenter.EnemyCaptured.Add(Berserk);
        GameManager.instance.stuffTakingFocus.Remove(this);

        UnSelect();
        highlightLocations.Clear();

        GameNoticationCenter.instance.runner.RunNext();
    }

    public void BerserkTargetSelected()
    {
        if (GetThreatLocations(true).Contains(InputManager.lastGridPoint))
        {
            //remove actions
            GameNoticationCenter.instance.ClickedPiece.Remove(BerserkTargetSelected);
            GameNoticationCenter.instance.RightClick.Remove(CancelBerserk);

            currentBerserkCons++;
            if (currentBerserkCons < maxBerserkCons)
            {
                player.noticationCenter.EnemyCaptured.Add(Berserk);
            }
            else
            {
                berserking = false;
            }

            CaptureEnemyPiece(GameManager.instance.boardLogic.map.SquareAt(InputManager.lastGridPoint).piece, InputManager.lastGridPoint);
            GameManager.instance.stuffTakingFocus.Remove(this);

            if (!berserking)
            {
                currentBerserkInstance++;
                if (currentBerserkInstance < maxBerserkInstance)
                {
                    player.noticationCenter.EnemyCaptured.Add(Berserk);
                }
            }
        }
        GameNoticationCenter.instance.runner.RunNext();
    }
    #endregion
}