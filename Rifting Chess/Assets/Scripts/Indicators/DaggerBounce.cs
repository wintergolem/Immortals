using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DaggerBounce : MonoBehaviour
{

    public float startYValue = 0; //set in editor
    public float endYValue = 0; //set in editor
    public float bounceSpeed = 0.1f; //set in editor

    bool bounceDown = true;
    void Start()
    {
        
    }

    void Update()
    {
        if(bounceDown)
        {
            transform.localPosition -= new Vector3(0,bounceSpeed,0);
            if (transform.localPosition.y <= endYValue) bounceDown = false;
        }
        else
        {
            transform.localPosition += new Vector3(0, bounceSpeed, 0);
            if (transform.localPosition.y >= startYValue) bounceDown = true;
        }
    }

    public void StartBounce()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, startYValue, transform.localPosition.z);
    }
}
