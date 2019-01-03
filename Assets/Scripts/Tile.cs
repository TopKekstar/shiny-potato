using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main tile class, father of all kind of tiles
/// </summary>
public class Tile : MonoBehaviour {
    public class TileInfo
    {
        public TileInfo()
        {
            position = new Vector2Int();
        }
        public Vector2Int position;
        public int _type;
        public int _touchs;
    }
    protected int _pendingTouchs;
    protected Vector2Int GridPosition;

    /// <summary>
    /// Overridable Method for initializiation of the Tile
    /// </summary>
    /// <param name="x">X position in the grid</param>
    /// <param name="y">Y position in the grid</param>
    /// <param name="touchs">Touchs you need for destroying that tile</param>
    public virtual void Init(int x , int y, int touchs)
    {
        GridPosition = new Vector2Int(x, y);
        _pendingTouchs = touchs;
        transform.Translate(x, y, 0);
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
    ///  Overridable method for notify the block the round is over.
    /// </summary>
    public virtual void EndOfRound()
    {
        transform.Translate(0, -1, 0);
        GridPosition.y++;
    }

    /// <summary>
    /// Overridable method for notifying the tile has been touched
    /// </summary>
    /// <returns>Returns if the tile is finally destroyed</returns>
    public virtual bool Touch()
    {
        _pendingTouchs--;
        return CanBeDestroyed()&&_pendingTouchs<=0;
    }

    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
