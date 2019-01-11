using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For the behaviour of the button of every level
/// </summary>
public class MenuButtonBehaviour : MonoBehaviour {
    private int _id;


    /// <summary>
    /// Set the id of the button
    /// </summary>
    /// <param name="id">the new value</param>
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
