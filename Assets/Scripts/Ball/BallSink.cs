using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component where the balls go to sleep
/// </summary>
public class BallSink : MonoBehaviour {
    /// <summary>
    /// The text that display the balls you can use
    /// </summary>
    private TextMesh text;

    /// <summary>
    /// The current balls that are dead
    /// </summary>
    public uint _deadBalls;

    public uint _numBalls;
    
    /// <summary>
    /// A delegate when all the balls reach to the destination
    /// </summary>
    public System.Action actionAllBallsReached;

    /// <summary>
    /// for checking if is waiting for the first ball
    /// </summary>
    public bool waitingFirstBall;

    /// <summary>
    /// Method for notifying when a ball reached the ball sink
    /// </summary>
    /// <param name="ball">The ball that reached the BallSink</param>
    public void BallReached(BallLogic ball)
    {        
        _deadBalls++;
        if (_deadBalls == _numBalls)
        {
            if (actionAllBallsReached != null)
            {
                actionAllBallsReached();
                UpdateText();
            }
        }
    }

    // Use this for initialization
    void Start () {
        waitingFirstBall = true;
        text = GetComponent<TextMesh>();
        UpdateText();
	}
	
    /// <summary>
    /// Method for notifying whe a ball is launched
    /// </summary>
    /// <param name="ball">The ball</param>
    public void BallLaunched(BallLogic ball)
    {
        _deadBalls--;
        text.text = "";

    }

    /// <summary>
    /// Method for updating the text 
    /// </summary>
    public void UpdateText()
    {
        text.text = (_deadBalls!=0)?_deadBalls.ToString():"";
    }
}
