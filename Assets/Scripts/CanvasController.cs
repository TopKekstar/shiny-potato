using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour {
    public RectTransform Up;
    public RectTransform Down;
    public RectTransform Left;
    public RectTransform Right;
	// Use this for initialization
	public void SetUpCanvas () {

        var techo= GameObject.Find("Techo");
        var suelo = GameObject.Find("DeathZone");
        var der = GameObject.Find("pared_der");
        var izq = GameObject.Find("pared_iz");


        //Sacamos la posición en el mundo de las paredes
        Vector2 techoPos = new Vector2(
            techo.transform.position.x,
            techo.transform.position.y - techo.GetComponent<Collider2D>().bounds.size.y / 2.0f
            );
        Vector2 sueloPos = new Vector2(
        suelo.transform.position.x,
        suelo.transform.position.y + suelo.GetComponent<Collider2D>().bounds.size.y / 2.0f
        );
        Vector2 izqPos = new Vector2(
            izq.transform.position.x + izq.GetComponent<Collider2D>().bounds.size.x / 2.0f,
            izq.transform.position.y
            );
        Vector2 derPos = new Vector2(
        der.transform.position.x - der.GetComponent<Collider2D>().bounds.size.x / 2.0f,
        der.transform.position.y
        );

        // Pasamos sus coordenadas a coordenadas de la pantalla
        var techoScreen = Camera.main.WorldToScreenPoint(techoPos);
        var sueloScreen = Camera.main.WorldToScreenPoint(sueloPos);
        var izqScreen = Camera.main.WorldToScreenPoint(izqPos);
        var derScreen = Camera.main.WorldToScreenPoint(derPos);


        //Haciendo uso de los anchors y los pivots, solo queda colocarlos en su lugar.
        Up.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)Screen.height - techoScreen.y);
        Down.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sueloScreen.y);
        Left.position = new Vector2(izqScreen.x, Screen.height/2);
        Right.position = new Vector2(derScreen.x, Screen.height/2);
        	}
	
	// Update is called once per frame
	void Update () {

    }
}
