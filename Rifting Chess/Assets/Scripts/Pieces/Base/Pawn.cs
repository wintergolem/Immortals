using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public void Awake()
    {
        value = 1;
        type = PieceType.Pawn;
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
