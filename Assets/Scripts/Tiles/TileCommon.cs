﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Son of tile, this tile is just a common tile nothing to see here
/// </summary>
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
                _dead = true;
                GetComponent<Collider2D>().enabled = false;
                StopCoroutine(DeathCouritine());
                StartCoroutine(DeathCouritine());

                
            }
            else
            {
                StopCoroutine(ChangeColorCouritine());
                StartCoroutine(ChangeColorCouritine());
            }
            updateText();
        }
    }

    /// <summary>
    /// Coroutine for changing the color when hit
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeColorCouritine()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            Color color = new Color( 0.01f,0.01f,0.01f);
            sprite.color = color;

            while (sprite.color.r < 1.0f)
            {
                sprite.color += color;
                yield return new WaitForEndOfFrame();
            }

        }
    }

    /// <summary>
    /// Courutine for animating the death.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathCouritine()
    {

        Vector3 amount = new Vector3(0.1f, 0.1f, 0.1f);
        while (transform.localScale.x < 1.5f)
        {
            transform.localScale += amount;
            yield return new WaitForEndOfFrame();
        }

        while (transform.localScale.x > -0.1)
        {
            transform.localScale -= amount;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);

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
