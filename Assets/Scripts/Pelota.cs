using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {
    private Rigidbody2D rigidbody;
    
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(1, 5);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
