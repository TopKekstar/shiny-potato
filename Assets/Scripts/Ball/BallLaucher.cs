using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component for launching balls
/// </summary>
public class BallLaucher : MonoBehaviour {
    private int _ballsLaunched;
    private int _ballsToLauch;
    public GameObject ball;
    /// <summary>
    /// Getter/Setter for the value _numBalls
    /// </summary>
    /// <value> Gets / Sets the value _numBalls</value>
    public uint NumBalls { get; set; }

    /// <summary>
    /// A list of all the balls
    /// </summary>
    public List<BallLogic> Balls;
    public BallSink ballSink;

	// Use this for initialization
	void Start () {
        Balls = new List<BallLogic>();
        GameObject aux = GameObject.Instantiate(ball, transform.position, Quaternion.identity);
        Balls.Add(aux.GetComponent<BallLogic>());
    }

    /// <summary>
    /// Start the Coroutine of launching balls
    /// </summary>
    /// <param name="direction">the direction of the ball</param>
    /// <param name="actionBallLaunched">A Delegate for notifying when a ball is Launched</param>
    public void LaunchBalls(Vector2 direction, System.Action<BallLogic> actionBallLaunched = null)
    {
        _ballsLaunched = 0;
        _ballsToLauch = (int)NumBalls;
        direction.Normalize();
        StartCoroutine(LaunchBallsCor(direction,actionBallLaunched));

    }

    /// <summary>
    /// Coroutine to launch all balls 
    /// </summary>
    /// <param name="dir">the direction of the ball</param>
    /// <param name="actionBallLaunched">A Delegate for notifying when a ball is Launched</param>
    /// <returns></returns>
    private IEnumerator LaunchBallsCor(Vector2 dir, System.Action<BallLogic> actionBallLaunched = null)
    {


        while (_ballsLaunched < _ballsToLauch)
        {
            try
            {
                Balls[_ballsLaunched].ShootBall(dir, 10.0f);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                GameObject aux = GameObject.Instantiate(ball, transform.position, Quaternion.identity);
                BallLogic currentBall = aux.GetComponent<BallLogic>();
                currentBall.ShootBall(dir, 10.0f);
                Balls.Add(currentBall);
            }
            
            if(actionBallLaunched!=null)actionBallLaunched(Balls[_ballsLaunched]);
                _ballsLaunched++;
            yield return new WaitForSeconds(0.1f);
        }

    }

    // Update is called once per frame
    void Update () {


		
	}

    public void SetLine(Vector2 vector)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, new Vector3(vector.x, vector.y));
        }
    }
}
