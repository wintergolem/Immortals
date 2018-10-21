using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    public int numOfRows; // x-wise, where pieces are deployed on
    public int numOfColumons; // y-wise, where pieces travel
    public GameObject zeroSpot;

    float width;
    float height;
    public float squareWidth;
    public float squareHeight;

    private void Awake()  {
        width = gameObject.GetComponent<MeshCollider>().bounds.size.x;
        height = gameObject.GetComponent<MeshCollider>().bounds.size.z;

        squareWidth = width / numOfRows;
        squareHeight = height / numOfColumons;

        Geometry.squareWidth = squareWidth;
        Geometry.squareHeight = squareHeight;
        Geometry.squareDepth = zeroSpot.transform.position.y;

        Geometry.zeroCoord = new Vector2(zeroSpot.transform.position.x,zeroSpot.transform.position.z);
    }
}
