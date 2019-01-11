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

    protected PowerUpManager powerUpManager;
    protected AudioSource audio;

    public uint _nBalls;
    protected int _pendingBalls;
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
            Vector3 newPos = new Vector3(ball.transform.position.x, ballSink.transform.position.y);
            ballSink.transform.position = newPos;
            _multiplier = 1;
            
        }
        ball.MoveTo(ballSink.transform.position, ballSink.BallReached);

    }

    public void AddPendingBalls(int balls)
    {
        _pendingBalls+= balls;
    }

    private void Awake()
    {
        nLevel = (short)GameManager.manager.LoadedLevel;
        powerUpManager = GetComponent<PowerUpManager>();
        audio = GetComponent<AudioSource>();
        
    }
    private void Start()
    {
        buildLevel(nLevel);   
    }

    /// <summary>
    /// Builds the level loading the map with index idx
    /// </summary>
    /// <param name="idx">Index of the map to load in the scene</param>
    public void buildLevel(int idx)
    {
        _pendingBalls = 0;
        deathZone.actionBallTouch = BallReachedDeathZone;
        nBalls = _nBalls;
        ballSink.actionAllBallsReached = AllBallsReached;
        _score = 0;
        _multiplier = 1;
        onPlay = true;


        nLevel = (short)idx;
        Tile.TileInfo[,] tileInfoMatrix;
        string path = "Mapas/mapdata" + idx.ToString();
        TextAsset text = Resources.Load<TextAsset>(path);
        tileInfoMatrix = LevelBuilder.ReadFile(text);
        boardManager.BuildMap(tileInfoMatrix, this);

        cam.GetComponent<ScalableCamera>().SetUpCamera();
        canvasC.SetUpCanvas();
        powerUpManager.Init(boardManager);
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
        if(boardManager.EndOfRound()) SceneManager.LoadScene(0);
        
        if (boardManager.MapFinished())
        { 
            GameManager.gameProgress.levelProgress levelP = new GameManager.gameProgress.levelProgress();

            levelP.score = _score;
            levelP.complete = true;
            levelP.unlocked = true;
            levelP.stars = 3;
            levelP.levelNumber = (short)(nLevel- 1);

            GameManager.manager.levelComplete(levelP);
        }
        
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
    /// <param name="tile"></param>
    public void onTileDestroyed(Tile tile)
    {
        switch (tile.type)
        {
            case 21:
                AddPendingBalls(1);
                break;
            case 22:
                AddPendingBalls(2);
                break;
            case 23:
                AddPendingBalls(3);
                break;
            default:
                break;
        }
        audio.Play();
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
