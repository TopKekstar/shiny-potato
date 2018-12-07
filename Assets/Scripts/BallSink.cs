using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSink : MonoBehaviour {
    private TextMesh text;

    public void BallReached(BallLogic ball)
    {
        Debug.Log("reached");
        
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
