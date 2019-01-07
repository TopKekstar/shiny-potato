using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCommonHard : TileCommon
{ 

    protected override void updateText()
    {
        text.text = (_pendingTouchs/2).ToString();
    }

}
