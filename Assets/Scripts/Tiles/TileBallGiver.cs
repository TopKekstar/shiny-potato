using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBallGiver : Tile
{
    public int nBallsDrop;

    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.GetComponent<BallLogic>())
        {
            if (Touch())
            {
                _dead = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(DeathCouritine());
            }

        }
    }

    public override bool Touch()
    {
        levelManager._pendingBalls+=nBallsDrop;
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



    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
