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

public class Knight : Piece
{
    public void Awake()
    {
        value = 3;
    }

    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        //y postive "forward"
        Vector2Int forwardLeft = new Vector2Int(gridPoint.x + 1, gridPoint.y + 2);

            locations.Add(forwardLeft);
      
        Vector2Int forwardRight = new Vector2Int(gridPoint.x - 1, gridPoint.y + 2);
      
            locations.Add(forwardRight);
   

        //y postive "backward"
        Vector2Int backwardLeft = new Vector2Int(gridPoint.x + 1, gridPoint.y - 2);
     
            locations.Add(backwardLeft);
       
        Vector2Int backwardRight = new Vector2Int(gridPoint.x - 1, gridPoint.y - 2);

            locations.Add(backwardRight);
      

        //x postive "left side"
        Vector2Int leftForward = new Vector2Int(gridPoint.x + 2, gridPoint.y + 1);
       
            locations.Add(leftForward);
       
        Vector2Int leftBackward = new Vector2Int(gridPoint.x  + 2, gridPoint.y - 1);

            locations.Add(leftBackward);


        //x postive "right side"
        Vector2Int rightForward = new Vector2Int(gridPoint.x - 2, gridPoint.y + 1);
     
            locations.Add(rightForward);
       
        Vector2Int rightBackward = new Vector2Int(gridPoint.x  - 2, gridPoint.y - 1);
       locations.Add(rightBackward);

        locations.RemoveAll(tile => tile.x < 0 || tile.x > 7 || tile.y < 0 || tile.y > 7);
        return locations;
    }
}
