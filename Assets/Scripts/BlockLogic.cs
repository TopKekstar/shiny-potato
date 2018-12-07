using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour { 
    public int lifes;

    Vector2 GridPosition;
    protected TextMesh text;
    public virtual bool MustBeDestroyed()
    {
        return true;
    }
    public bool IsDead()
    {
        return lifes < 1 && MustBeDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            lifes = lifes - 1;
           
            if(lifes <= 0 && MustBeDestroyed())
            {
                gameObject.SetActive(false);
            }
            else
            {
                updateText();
            }
        }
    }

    public void SetProperties(uint x, uint y, int lifes)
    {

    }

    // Use this for initialization
    void Start () {
        lifes = 2;
        text = GetComponentInChildren<TextMesh>();
        updateText();

    }

    void updateText()
    {
        if(MustBeDestroyed()) text.text = lifes.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
