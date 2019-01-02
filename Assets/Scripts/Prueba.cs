using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine;

public class Prueba : MonoBehaviour {
    public GameObject button;
    public int numberOfElements = 14;
	// Use this for initialization
	void Awake () {
    
        var cX = (Screen.width*0.99) / GetComponent<GridLayoutGroup>().constraintCount;
        var cSize = GetComponent<GridLayoutGroup>().cellSize;

        GetComponent<LayoutElement>().minWidth = (float)cX;
        cSize.Set((float)cX, cSize.y);




        for (var i = 0; i < numberOfElements; i++)
        {
            var x = Instantiate(button, gameObject.transform);
            x.SetActive(true);

        }
	}
    // Update is called once per frame
    void Update () {
		
	}
}
