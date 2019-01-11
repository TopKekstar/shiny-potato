using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// For making the select level menu
/// </summary>
public class ListController : MonoBehaviour {
    public GameObject element;
    public int numberElements = 150;


   

	// Use this for initialization
	void Start() {
        var gridLG = gameObject.GetComponent<GridLayoutGroup> ();
        var squareSide = Mathf.Floor(gameObject.GetComponent<RectTransform>().rect.width / (gridLG.constraintCount+1f));

        gridLG.cellSize = new Vector2(squareSide, squareSide);
        gridLG.spacing = new Vector2(squareSide/10, squareSide/5);


        GameManager.gameProgress l = GameManager.manager.GetGameProgress();


        int i = 0;
        foreach (var level in l.Progresses)
        {
            var z = Instantiate(element, gameObject.transform);
            if (!level.unlocked)
            {
                z.GetComponent<Button>().interactable = false;
                foreach (Transform child in z.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                z.GetComponent<MenuButtonBehaviour>().setButtonId(i + 1);
                z.GetComponentInChildren<Text>().text = (i + 1).ToString();
                z.GetComponentInChildren<Text>().fontSize = (int)Mathf.Floor((1 / 3f) * squareSide);

                Transform starZone = z.transform.GetChild(0);


                int k = level.stars;
                while(k > 0)
                {
                    starZone.GetChild(k-1).gameObject.SetActive(true);
                    k--;
                }
            }
            i++;
        }
	}
}
