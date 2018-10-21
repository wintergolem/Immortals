/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public void Awake()
    {
        value = 1;
    }

    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint) {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameManager.instance.players[playerIndex].forward;
        var indexes = forwardDirection > 0 ? new[] {7,0,1} : new[]{3,4,5};

        Square neig = square.neighbors[indexes[1]];
        if (neig != null && neig.Empty) 
            locations.Add(square.neighbors[indexes[1]].personalCoord);
        if (!hasMoved && CheckNeighors(indexes[1], 2)) {
            locations.Add(GetNeighbor(indexes[1], 2).personalCoord);
          //  print("Added far forward");
        }

        neig = square.neighbors[indexes[0]];
        if (neig != null && !neig.Empty)
            if (neig.piece.playerIndex != playerIndex)
                locations.Add(neig.personalCoord);
        neig = square.neighbors[indexes[2]];
        if (neig != null && !neig.Empty)
            if (neig.piece.playerIndex != playerIndex)
                locations.Add(neig.personalCoord);
        return locations;
    }

    public override List<Vector2Int> GetThreatLocations() {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameManager.instance.players[playerIndex].forward;
        Square forwardNeighor = square.neighbors[ forwardDirection > 0 ? 0 : 4];
        Square right = forwardNeighor.neighbors[2];
        Square left = forwardNeighor.neighbors[6];
        if (right != null)
            locations.Add(right.personalCoord);
        if (left != null) 
            locations.Add(left.personalCoord);

        return locations;
    }

}
