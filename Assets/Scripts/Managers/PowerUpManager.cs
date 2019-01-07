using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager for controlling the power-ups
/// </summary>
public class PowerUpManager : MonoBehaviour
{

    private BoardManager boardManager;

    


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
    
    /// <summary>
    /// This power-up will erase the last row with blocks
    /// </summary>
    public void DoEraseRow()
    {

    }

    /// <summary>
    /// this power-up will put a barrier of Tile of steel (The unbreakables ones)
    /// </summary>
    public void DoSteelBarrier()
    {

    }

    /// <summary>
    /// For notifying the power-up manager the end of the round
    /// </summary>
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