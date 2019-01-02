using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonBehaviour : MonoBehaviour {
    private int _id;

    public void setButtonId(int id)
    {
        _id = id;

    }
    void Awake () {
        _id = -1;
		
	}
    public void OnClick()
    {
        GameManager.manager.loadLevel(_id);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
