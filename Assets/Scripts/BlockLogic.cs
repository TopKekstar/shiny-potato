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

    protected int lifes;

    Vector2Int GridPosition;
    protected TextMesh text;
    public virtual bool MustBeDestroyed()
    {
        return true;
    }

 
    public bool IsDead()
    {
        return lifes < 1 ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            lifes = lifes - 1;
           
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
