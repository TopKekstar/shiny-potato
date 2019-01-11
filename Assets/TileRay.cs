using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRay : Tile
{
    int counterIn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallLogic>())
        {
            counterIn++;
            if(counterIn == 1)
            {
                ray.SetActive(true);
            }
            Hit();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BallLogic>())
        {
            counterIn--;
            if (counterIn <= 0)
            {
                ray.SetActive(false);
            }
        }
    }
    public GameObject ray;

    public enum OrientationRay
    {
        Horizontal,Vertical
    };
    public OrientationRay orientationRay;

    public override void Hit()
    {
        if (Touch())
        {
            _dead = true;
            ActionWhenHit(this);
        }
    }

    public override void Kill()
    {
        _dead = true;
    }
    private void Start()
    {
        counterIn = 0;
    }
}
