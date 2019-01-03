using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCommon : Tile
{
    protected TextMesh text;

    /// <summary>
    /// for detecting the collision enter
    /// </summary>
    /// <param name="collision">the object that collides</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallLogic>() != null)
        { 
            if (Touch())
            {
                gameObject.SetActive(false);
            }
            else
            {
                updateText();
            }
        }
    }

    /// <summary>
    /// For updating the text with the pending touchs 
    /// </summary>
    private void updateText()
    {
        text.text = _pendingTouchs.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMesh>();
        updateText();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
