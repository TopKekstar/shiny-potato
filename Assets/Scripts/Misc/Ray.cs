using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// just for the TileRay
/// </summary>
public class Ray : MonoBehaviour
{
    public TileRay.OrientationRay orientation;
    private Vector3 scaler;
    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float variation = 1.0f+(float)System.Math.Sin(Time.renderedFrameCount)*0.3f;
        switch (orientation)
        {
            case TileRay.OrientationRay.Horizontal:
                scaler.y = variation;
                break;
            case TileRay.OrientationRay.Vertical:
                scaler.x = variation;
                break;
        }
        transform.localScale = scaler;
    }
}