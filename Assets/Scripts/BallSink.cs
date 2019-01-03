using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component where the balls go to sleep
/// </summary>
public class BallSink : MonoBehaviour {
    private TextMesh text;
    private Vector2 _ballPos;
    public uint _deadBalls;
    public uint _numBalls;
    public LevelManager levelManager;
    public System.Action actionAllBallsReached;

    /// <summary>
    /// Method for notifying when a ball reached the ball sink
    /// </summary>
    /// <param name="ball">The ball that reached the BallSink</param>
    public void BallReached(BallLogic ball)
    {        
        _deadBalls++;
        UpdateText();
        if (_deadBalls == _numBalls)
        {
            if (actionAllBallsReached != null)
            {
                actionAllBallsReached();
            }
        }
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        text = GetComponent<TextMesh>();
        UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Method for notifying whe a ball is launched
    /// </summary>
    /// <param name="ball">The ball</param>
    public void BallLaunched(BallLogic ball)
    {
        _deadBalls--;
        UpdateText();
    }

    /// <summary>
    /// Method for updating the text 
    /// </summary>
    private void UpdateText()
    {
        text.text = (_deadBalls!=0)?_deadBalls.ToString():"";
    }
}
