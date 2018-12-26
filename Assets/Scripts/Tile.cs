using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    LevelManager _levelManager;
    protected int _pendingTouchs;

    public virtual void Init(LevelManager manager)
    {
        _levelManager = manager;
    }

    
    public virtual bool CanFall()
    {
        return true;
    }
    public virtual bool CanBeDestroyed()
    {
        return true;
    }

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
