using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {
    float gameX = 11.25f;
    float gameY = 14f;

	// Use this for initialization
	void Start ()
    {
        Canvas c = gameObject.GetComponent<Canvas>();

        Debug.Log(Screen.height);
        Debug.Log(Screen.width);

        float aspectRatio = ((float)Screen.width / Screen.height);
        float gameAspectRatio = gameX / gameY;
        Debug.Log(gameAspectRatio);

        //GameObject gArea = GameObject.FindGameObjectWithTag("GameArea");
        

        if (gameAspectRatio < aspectRatio)
        {
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
