using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unbreakable tile, cannot go down
/// </summary>
public class TileSteel : Tile
{
    /// <summary>
    /// Cannot be destroyed
    /// </summary>
    /// <returns>Always false</returns>
    public override bool CanBeDestroyed()
    {
        return false;
    }

    /// <summary>
    /// Cannot go down
    /// </summary>
    /// <returns>Always false</returns>
    public override bool CanFall()
    {
        return false;
    }

    /// <summary>
    /// don't do nothing
    /// </summary>
    /// <returns>Always false</returns>
    protected override bool Touch()
    {
        return false;
    }
}
