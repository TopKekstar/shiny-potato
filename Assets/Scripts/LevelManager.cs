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
    public uint _score;
    public UnityEngine.UI.Text score;
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


    private short nLevel;
    private uint _multiplier;
    
    
    
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
            _multiplier = 1;
            
        }
        ball.MoveTo(ballSink.transform.position, ballSink.BallReached);

    }
    private void Awake()
    {
        _pendingBalls = 0;
        deathZone.actionBallTouch = BallReachedDeathZone;

        ballSink.actionAllBallsReached = AllBallsReached;
        nBalls = 20;
        _score = 0;
        _multiplier = 1;
        onPlay = true;
    }
    // Use this for initialization
    void Start () {
    }

    
    public void buildLevel(int idx)
    {
        nLevel = (short)idx;
        Tile.TileInfo[,] tileInfoMatrix;
        string path = "Mapas/mapdata" + idx.ToString();
        TextAsset text = Resources.Load<TextAsset>(path);
        tileInfoMatrix = LevelBuilder.ReadFile(text);
        boardManager.BuildMap(tileInfoMatrix,this);

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
        if(boardManager.MapFinished())
        {
            GameManager.gameProgress.levelProgress levelP = new GameManager.gameProgress.levelProgress();
            levelP.score = _score;
            levelP.complete = true;
            levelP.stars = 3;
            levelP.levelNumber = nLevel;
            GameManager.manager.modifyLevelProgress(levelP);
        }
        onPlay = true;
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
    /// This function calculates the new score after a tile is destroyed by a ball
    /// </summary>
    public void onTileDestroyed()
    {
        _score += _multiplier * 10;
        _multiplier += 1;
        score.text = _score.ToString();

    }

    /// <summary>
    /// Notify when a ball is launched
    /// </summary>
    void BallLaunched(BallLogic ball)
    { 
        ballSink.BallLaunched(ball);
    }

    



}
