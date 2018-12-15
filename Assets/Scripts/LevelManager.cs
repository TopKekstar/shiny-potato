using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public BallSink ballSink;
    public BallLaucher ballLauncher;
    public DeathZone deathZone;
    public BoardManager boardManager;
    public uint _nBalls;

    public uint nBalls
    {
        get
        {
            return _nBalls;
        }
        set
        {
            _nBalls = value;
            ballSink._numBalls = value;
            ballLauncher.NumBalls = value;

        }
    }
    public bool onPlay;

    /// <summary>
    /// Notify when all balls reach the BallSink
    /// </summary>
    private void AllBallsReached()
    {
        EndRound();
        boardManager.MapFinished();
    }

    /// <summary>
    /// Notify when a ball reach the DeathZone
    /// </summary>
    /// <param name="ball">the ball that reach the DeathZone</param>
    private void BallReachedDeathZone(BallLogic ball)
    {
        if(ballSink._deadBalls == 0)
        {
            ballSink.transform.position = ball.transform.position;
            ballLauncher.transform.Translate(ball.transform.position.x - ballLauncher.transform.position.x,0,0); 
        }
        ball.MoveTo(ballSink.transform.position, ballSink.BallReached);

    }
    private void Awake()
    {
        deathZone.actionBallTouch = BallReachedDeathZone;

        ballSink.actionAllBallsReached = AllBallsReached;

        ballSink._numBalls = nBalls;
        ballSink._deadBalls = nBalls;
        onPlay = true;
        ballLauncher.NumBalls = nBalls;
    }
    // Use this for initialization
    void Start () {
        BlockLogic.BlockInfo[,] blockInfo;
        TextAsset text = Resources.Load<TextAsset>("Mapas/mapdata1");
        blockInfo = LevelBuilder.ReadFile(text);
        boardManager.BuildMap(blockInfo);

    }

    /// <summary>
    /// For launching all the balls
    /// </summary>
    /// <param name="dir"></param>
	private void LaunchBalls(Vector2 dir)
    {
        onPlay = false;
        ballLauncher.LaunchBalls(dir, BallLaunched);

    }
    /// <summary>
    /// Is called once per frame, this is a method from MonoBehaviour
    /// </summary>
    void Update () {

    }

    void EndRound()
    {
        onPlay = true;
    }
  
    private void OnMouseDrag()
    {
       
    }
    private void OnMouseUp()
    {
        if (onPlay)
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //TODO:SHOOT
            v = v - ballSink.transform.position;
            LaunchBalls(new Vector2(v.x, v.y));
        }

    }

    /// <summary>
    /// Notify when a ball is launched
    /// </summary>
    void BallLaunched(BallLogic ball)
    { 
        ballSink.BallLaunched(ball);
    }

    



}
