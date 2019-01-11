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
        if (GameManager.manager.CanAffordPrice(100))
        {
            GameManager.manager.RemoveKeKCoins(100);
            boardManager.Earthquake();

        }
    }
    
    /// <summary>
    /// This power-up will erase the last row with blocks
    /// </summary>
    public void DoEraseRow()
    {
        if (GameManager.manager.CanAffordPrice(100))
        {
            GameManager.manager.RemoveKeKCoins(100);
            boardManager.EraseLastRow();
        }
    }

    /// <summary>
    /// this power-up will put a barrier of Tile of steel (The unbreakables ones)
    /// </summary>
    public void DoSteelBarrier()
    {
        if (GameManager.manager.CanAffordPrice(100))
        {
            GameManager.manager.RemoveKeKCoins(100);
            boardManager.SteelBarrier();
        }
    }

    /// <summary>
    /// For notifying the power-up manager the end of the round
    /// </summary>
    public void EndOfRound()
    {

    }
}