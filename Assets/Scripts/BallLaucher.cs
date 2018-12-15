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
    


    private int _ballsLaunched;
    public GameObject ball;
    /// <summary>
    /// Getter/Setter for the value _numBalls
    /// </summary>
    /// <value> Gets / Sets the value _numBalls</value>
    public  int NumBalls
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
    public void LaunchBalls(Vector2 direction)
    {
        _ballsLaunched = 0;
        StartCoroutine(LaunchBallsCor(direction));

    }
    
    private IEnumerator LaunchBallsCor(Vector2 dir)
    {

        while (_ballsLaunched < _numBalls)
        {
            GameObject aux = GameObject.Instantiate(ball,transform.position,Quaternion.identity);
            aux.GetComponent<BallLogic>().ShootBall(dir, 3.0f);
            _ballsLaunched++;
            yield return new WaitForSeconds(0.1f);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
