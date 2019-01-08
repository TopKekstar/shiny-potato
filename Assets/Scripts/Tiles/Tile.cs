using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main tile class, father of all kind of tiles
/// </summary>
public class Tile : MonoBehaviour {

    /// <summary>
    /// Struct for building the map
    /// </summary>
    public class TileInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TileInfo()
        {
            position = new Vector2Int();
        }
        /// <summary>
        /// Position in the grid
        /// </summary>
        public Vector2Int position;
        /// <summary>
        /// The type of tiles
        /// </summary>
        public int _type;
        /// <summary>
        /// the touches that need for destroying the tile
        /// </summary>
        public int _touchs;
    }

    /// <summary>
    /// Modifiable value of the current status of the tile
    /// </summary>
    protected bool _dead;

    /// <summary>
    /// Public variable for knowing if the tile is dead or alive
    /// </summary>
    public bool Dead
    {
        get
        {
            return _dead;
        }
    }

    /// <summary>
    /// The pending touches for destroying the tile
    /// </summary>
    protected int _pendingTouchs;

    /// <summary>
    /// The grid postion
    /// </summary>
    public Vector2Int GridPosition;

    /// <summary>
    /// the level manager for notifyning when the block is dead
    /// </summary>
    protected LevelManager levelManager;

    /// <summary>
    /// Overridable Method for initializiation of the Tile
    /// </summary>
    /// <param name="x">X position in the grid</param>
    /// <param name="y">Y position in the grid</param>
    /// <param name="touchs">Touchs you need for destroying that tile</param>
    public virtual void Init(int x , int y, int touchs, LevelManager lm)
    {
        _dead = false;
        levelManager= lm;
        GridPosition = new Vector2Int(x, y);
        _pendingTouchs = touchs;
    }

    /// <summary>
    /// Overridable method for checking if the block can fall
    /// </summary>
    /// <returns>By default returns true</returns>
    public virtual bool CanFall()
    {
        return true;
    }


    /// <summary>
    /// Overridable method for checking if the block can be destroyed
    /// </summary>
    /// <returns>Returns true by default</returns>
    public virtual bool CanBeDestroyed()
    {
        return true;
    }

    /// <summary>
    ///  Overridable method for notifying the block the round is over.
    /// </summary>
    public virtual void EndOfRound()
    {
       
    }

    /// <summary>
    /// Overridable method for notifying the tile has been touched
    /// </summary>
    /// <returns>Returns if the tile has no more pending touchs</returns>
    protected virtual bool Touch()
    {
        _pendingTouchs--;
        return CanBeDestroyed()&&_pendingTouchs<=0;
    }

    /// <summary>
    /// Overridable method for simulating a touch
    /// </summary>
    public virtual void Hit()
    {
        Touch();
    }

    //Kill that tile
    public virtual void Kill()
    {
        _pendingTouchs = 0;
        Hit();
        gameObject.SetActive(false);
    }

}
