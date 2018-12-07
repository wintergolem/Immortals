using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public void Awake()
    {
        value = 5;
        type = PieceType.Rook;
    }
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (int index in RookIndexes)
        {
            Square check = square;
            do
            {
                Square next = check.neighbors[index];
                if (next == null)
                    break;
                locations.Add(next.personalCoord);
                check = next;
            } while (check.Empty);
        }

        return locations;
    }
}
