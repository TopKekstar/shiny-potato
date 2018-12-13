using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public BallSink ballSink;
    public BallLaucher ballLauncher;
    public DeathZone deathZone;
    public int nBalls;
    // Use this for initialization
    void Start () {
        ballSink._numBalls = nBalls;
        ballLauncher.NumBalls = nBalls;
        ballLauncher.LaunchBalls(new Vector2(10, 10));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
