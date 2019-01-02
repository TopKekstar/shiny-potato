using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour {
    Canvas c;
    public RectTransform Up;
    public RectTransform Down;
    public RectTransform Left;
    public RectTransform Right;
	// Use this for initialization
	void Start () {
        c = this.GetComponent<Canvas>();


        var techo= GameObject.Find("Techo");
        var suelo = GameObject.Find("DeathZone");
        var der = GameObject.Find("pared_der");
        var izq = GameObject.Find("pared_iz");
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
