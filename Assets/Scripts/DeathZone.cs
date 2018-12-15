using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour { 

    /// <summary>
    /// A Delegate for sending the event when a ball enters the DeathZone
    /// </summary>
    public System.Action<BallLogic> actionBallTouch;

    /// <summary>
    /// Detects when somenthing enters the death zone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallLogic pelota = collision.GetComponent<BallLogic>();
        if (pelota!=null)
        {
            pelota.Stop();
            if (actionBallTouch != null)
            {
                actionBallTouch(pelota);
                
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
