using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ListController : MonoBehaviour {

    [SerializeField]
    public GameObject element;
    public int numberElements = 150;


   

	// Use this for initialization
	void Start () {
        var gridLG = gameObject.GetComponent<GridLayoutGroup> ();
        var squareSide = Mathf.Floor(gameObject.GetComponent<RectTransform>().rect.width / (gridLG.constraintCount+1f));

        gridLG.cellSize = new Vector2(squareSide, squareSide);
        gridLG.spacing = new Vector2(squareSide/10, squareSide/5);
        

     
        


        for (int i = 0; i < numberElements; i++)
        {
            var z = Instantiate(element, gameObject.transform);
            z.GetComponent<MenuButtonBehaviour>().setButtonId(i+1);
           // z.GetComponent<Button>().interactable = false;
            z.GetComponentInChildren<Text>().text = (i+1).ToString();
            z.GetComponentInChildren<Text>().fontSize = (int)Mathf.Floor((1/3f)* squareSide);
           
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
