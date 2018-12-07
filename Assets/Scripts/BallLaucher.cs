using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaucher : MonoBehaviour {
    /// <summary>
    /// The number of balls
    /// </summary>
    /// <value>
    /// The number of balls
    /// </value>
    private int _numBalls;
   
    /// <summary>
    /// Getter/Setter for the value _numBalls
    /// </summary>
    /// <value> Gets / Sets the value _numBalls</value>
    public int NumBalls
    {
        get
        {
            return _numBalls;
        }
        set
        {
            _numBalls = value;
        }
    }

    public BallSink ballSink;

	// Use this for initialization
	void Start () {
        NumBalls = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
