using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBallGiver : Tile
{
    public int nBallsDrop;

    private void OntriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.GetComponent<BallLogic>())
        {
            if (Touch())
            {
                
            }

        }
    }

    public override bool Touch()
    {
        Debug.Log("I should give you balls, but fuck you.");
        return true;
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
