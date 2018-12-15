using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    public BallSink ballSink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallLogic pelota = collision.GetComponent<BallLogic>();
        if (pelota!=null)
        {
            pelota.Stop();
            pelota.MoveTo(ballSink.transform.position,ballSink.BallReached);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
