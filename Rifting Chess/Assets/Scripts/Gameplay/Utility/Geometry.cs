
using UnityEngine;
/*
public class Geometry
{
    public static float squareWidth = 0;
    public static float squareHeight = 0;
    public static float squareDepth = 0;
    public static Vector2 zeroCoord = new Vector2(0,0);

    static public Vector3 PointFromGrid(Vector2Int gridPoint) {
        float x = zeroCoord.x + (squareWidth / 2 ) + 1.0f * (gridPoint.x * squareWidth );
        float z = zeroCoord.y + (squareHeight / 2 ) +1.0f * (gridPoint.y * squareHeight);
        return new Vector3(x, squareDepth, z);
    }

    static public Vector2Int GridPoint(int col, int row) {
        return new Vector2Int(col, row);
    }

    static public Vector2Int GridFromPoint(Vector3 point) {
        // x - zeroCoord.x
        Vector2 p = new Vector2(point.x - zeroCoord.x , point.z - zeroCoord.y );
        Vector2Int value =  new Vector2Int( (int)(p.x / squareWidth) , (int)( p.y / squareHeight));
        //Debug.Log(point.ToString() + " - " + p.ToString() + " - " + value.ToString());
        return value;

        //int col = Mathf.FloorToInt(4.0f + point.x);
        //int row = Mathf.FloorToInt(4.0f + point.z);
       // return new Vector2Int(col, row);
    }
}

    */

public class TileAppearance
{
    public static GameObject tileHighlightPrefab;
    public static GameObject moveLocationPrefab;
    public static GameObject attackLocationPrefab;

}