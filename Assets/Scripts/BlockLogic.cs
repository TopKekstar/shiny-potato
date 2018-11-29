using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour {
    protected bool killable;
    public int lifes;
    protected TextMesh text;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            lifes = lifes - 1;
            updateText();
            if(lifes <= 0 && killable)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start () {
        killable = true;
        lifes = 2;
        text = GetComponentInChildren<TextMesh>();
        updateText();

    }

    void updateText()
    {
        if(killable)
            text.text = lifes.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
