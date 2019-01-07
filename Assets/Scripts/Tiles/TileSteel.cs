using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSteel : Tile
{
    public override bool CanBeDestroyed()
    {
        return false;
    }

    public override bool CanFall()
    {
        return false;
    }

    protected override bool Touch()
    {
        return false;
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
