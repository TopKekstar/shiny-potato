using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUp_t
    {
        None,
        Earthquake,
        SteelTiles,
    }
    BoardManager boardManager;

    


    public void Init(BoardManager bm)
    {
        boardManager = bm;
    }

    /// <summary>
    /// This power-up make an earthquake that hits 
    /// </summary>
    public void DoEarthquake()
    {
        boardManager.Earthquake();
    }

    public void DoErase()
    {

    }

    public void EndOfRound()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}