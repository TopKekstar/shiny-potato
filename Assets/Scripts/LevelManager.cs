using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Debug
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public Camera cam;
    public CanvasController canvasC;
    public BallSink ballSink;
    public BallLaucher ballLauncher;
    public DeathZone deathZone;
    public BoardManager boardManager;
    public uint _nBalls;
    public int _pendingBalls;

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
            ballSink._deadBalls = value;
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
    }

    /// <summary>
    /// Notify when a ball reach the DeathZone
    /// </summary>
    /// <param name="ball">the ball that reach the DeathZone</param>
    private void BallReachedDeathZone(BallLogic ball)
    {
        if(ballSink.waitingFirstBall)
        {
            ballSink.waitingFirstBall = false;
            ballSink.transform.position = ball.transform.position;
            
        }
        ball.MoveTo(ballSink.transform.position, ballSink.BallReached);

    }
    private void Awake()
    {
        _pendingBalls = 0;
        deathZone.actionBallTouch = BallReachedDeathZone;

        ballSink.actionAllBallsReached = AllBallsReached;
        nBalls = 20;
        onPlay = true;
    }


    public void buildLevel(int idx)
    {
        Tile.TileInfo[,] tileInfoMatrix;
        string path = "Mapas/mapdata" + idx.ToString();
        TextAsset text = Resources.Load<TextAsset>(path);
        tileInfoMatrix = LevelBuilder.ReadFile(text);
        boardManager.BuildMap(tileInfoMatrix, this);

        cam.GetComponent<ScalableCamera>().SetUpCamera();
        canvasC.SetUpCanvas();
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


    /// <summary>
    /// Notify the end of the round and check the end of the level
    /// </summary>
    void EndRound()
    {
        onPlay = true;
        if (boardManager.EndOfRound())
            Debug.Log("Game Over chacho");
        boardManager.MapFinished();
        nBalls += (uint)_pendingBalls;
        _pendingBalls = 0;
        ballSink.UpdateText();
        ballSink.waitingFirstBall = true;
        ballLauncher.transform.Translate(ballSink.transform.position.x - ballLauncher.transform.position.x, 0, 0);

    }

    private void OnMouseDrag()
    {
       
    }
    private void OnMouseUp()
    {
        if (onPlay)
        {
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
