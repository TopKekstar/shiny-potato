using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour {

    public class BlockInfo
    {
        public BlockInfo()
        {
            position = new Vector2Int();
        }
        public Vector2Int position;
        public int type;
        public int lifes;
    }
    Vector2Int GridPosition;
    protected TextMesh text;
    protected int lifes;

    /// <summary>
    /// Returns when the block must be destroyed in order to en the level
    /// </summary>
    /// <returns> if the block must be destroyed</returns>
    public virtual bool MustBeDestroyed()
    {
        return true;
    }

    /// <summary>
    /// Virtual method to indicate something touched the block and must do something
    /// </summary>
    public virtual void Touch()
    {
        lifes = lifes - 1;
         
        
    }

    /// <summary>
    /// Indicates the block when the round is over
    /// </summary>
    public virtual void EndOfRound()
    {

    }

 
    /// <summary>
    /// to indicate if the block is dead.
    /// </summary>
    /// <returns>if the block is dead</returns>
    public bool IsDead()
    {
        return lifes < 1 ;
    }


    /// <summary>
    /// To detect the collision enter
    /// </summary>
    /// <param name="collision">the object that collides</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Touch();
           
            if(IsDead() && MustBeDestroyed())
            {
                gameObject.SetActive(false);
            }
            else
            {
                updateText();
            }
        }
    }

    /// <summary>
    /// Initialitiazion method
    /// </summary>
    /// <param name="position">initial position</param>
    /// <param name="li">stands for lifes</param>
    public void SetProperties(Vector2Int position, int li)
    {
        GridPosition = new Vector2Int(position.x, position.y);
        lifes = li;
        transform.Translate(position.x, -position.y, 0);
    }

    // Use this for initialization
    void Start () {
        text = GetComponentInChildren<TextMesh>();
        updateText();

    }

    void updateText()
    {
        if(MustBeDestroyed()) text.text = lifes.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
