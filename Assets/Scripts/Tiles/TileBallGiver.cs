using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tile that when you hit it, give you more balls to play with it
/// </summary>
public class TileBallGiver : Tile
{
    /// <summary>
    /// The number of balls that give you when dies
    /// </summary>
    public int nBallsDrop;

    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.GetComponent<BallLogic>())
        {
            Hit();
        }
    }

    public override void Hit()
    {
        if (Touch()&&!Dead)
        {
            _dead = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DeathCouritine());
            ActionWhenDead(this);
        }
    }

    protected override bool Touch()
    {
        return true;
    }

    private IEnumerator DeathCouritine()
    {
        float amountR = 10.0f;
        Vector3 amount = new Vector3(0.1f, 0.1f, 0.1f);
        while (transform.localScale.x < 1.8f)
        {
            transform.localScale += amount;
            transform.Rotate(0.0f, 0.0f, amountR);
            amountR += 10.0f;
            yield return new WaitForEndOfFrame();
        }

        while (transform.localScale.x > -0.1)
        {
            transform.localScale -= amount;
            transform.Rotate(0.0f, 0.0f, amountR);
            amountR -= 20.0f;

            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
   
}
