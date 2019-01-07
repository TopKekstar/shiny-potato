using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tile is just the common tile, but shows the half of the touches that need to go down
/// </summary>
public class TileCommonHard : TileCommon
{ 

    /// <summary>
    /// The update text is almost like the TileCommon, but shows always the half of the current touches that needs
    /// </summary>
    protected override void updateText()
    {
        text.text = (_pendingTouchs/2).ToString();
    }

}
