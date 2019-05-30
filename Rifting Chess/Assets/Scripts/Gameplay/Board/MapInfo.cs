using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    public int[] numberOfSquares = new int[2];
    public GameObject zeroSpot;
    public GameObject boardObject;

    float width;
    float height;
    float squareWidth = 1.25f;
    float squareHeight = 1.25f;

    private void Awake()  {
        width = boardObject.GetComponent<MeshRenderer>().bounds.size.x;
        height = boardObject.GetComponent<MeshRenderer>().bounds.size.z;

        numberOfSquares[0] = (int)(width / squareWidth);
        numberOfSquares[1] = (int)(height / squareHeight);

        Geometry.squareWidth = squareWidth;
        Geometry.squareHeight = squareHeight;
        Geometry.squareDepth = zeroSpot.transform.position.y;

        Geometry.zeroCoord = new Vector2(zeroSpot.transform.position.x,zeroSpot.transform.position.z);
    }
}
