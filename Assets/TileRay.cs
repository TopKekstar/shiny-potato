using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRay : Tile
{
    public GameObject ray;

    protected AudioSource audio;

    public enum OrientationRay
    {
        Horizontal, Vertical
    };
    public OrientationRay orientationRay;

    /// <summary>
    /// Atribute for knowing how many balls still in the trigger
    /// </summary>
    private int counterIn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallLogic>())
        {
            if(!audio.isPlaying)
                audio.Play();

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
        audio = GetComponent<AudioSource>();
    }
}
