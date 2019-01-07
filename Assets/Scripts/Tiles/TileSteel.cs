using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unbreakable tile, cannot go down
/// </summary>
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
}
