using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSink : MonoBehaviour {
    private TextMesh text;
    private Vector2 _ballPos;
    private int _deadBalls;
    public int _numBalls;

    public void BallReached(BallLogic ball)
    {
        if(_deadBalls == 0)
        {
            _ballPos = ball.transform.position;
            _deadBalls++;

        }
        else if(_deadBalls == _numBalls)
        {
            _deadBalls = 0;
        }
        
    }

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
