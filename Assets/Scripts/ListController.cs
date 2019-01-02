using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ListController : MonoBehaviour {

    [SerializeField]
    public GameObject element;
    public int numberElements;
    public Canvas c;


   

	// Use this for initialization
	void Start () {
        var gridLG = gameObject.GetComponent<GridLayoutGroup> ();
        var squareSide = Mathf.Floor(gameObject.GetComponent<RectTransform>().rect.width / (gridLG.constraintCount+1f));

        Debug.Log(gameObject.GetComponent<RectTransform>().rect.width);

        gridLG.cellSize = new Vector2(squareSide, squareSide);
        gridLG.spacing = new Vector2(squareSide/10, squareSide/5);
        

        
        var eLE = element.GetComponent<LayoutElement>();
        


        for (int i = 0; i < numberElements; i++)
        {
            var z = Instantiate(element, gameObject.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
