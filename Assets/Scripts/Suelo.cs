using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suelo : MonoBehaviour {
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>("Ball"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GameManager.manager.launchPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);

        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
