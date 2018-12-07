using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The main logic of the ball
/// </summary>
public class BallLogic : MonoBehaviour {
    
    private Rigidbody2D rigidbody;

    /// <summary>
    /// Stops the ball right in the moment
    /// </summary>
    public void Stop()
    {
        rigidbody.velocity = new Vector2(0, 0);
    }
    /// <summary>
    /// Method to start the coroutine for moving the ball
    /// </summary>
    /// <param name="destiny">The destiny of the movement </param>
    /// <param name="action">The action that will do when reaches the destination</param>
    /// 
    public void MoveTo(Vector3 destiny, System.Action<BallLogic> action = null)
    {
        StartCoroutine(MoveToCoroutine(destiny,action));
        
    }

    /// <summary>
    /// Coroutine to move the ball to a indicated position
    /// </summary>
    /// <param name="destiny">The destiny of the movement </param>
    /// <param name="action">The action that will do when reaches the destination</param>
    /// <returns>The return value its for the coroutine </returns>
    private IEnumerator MoveToCoroutine(Vector3 destiny, System.Action<BallLogic> action = null)
    {
        while (Vector3.Distance(destiny, transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destiny, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        transform.position = Vector3.MoveTowards(transform.position, destiny, 1.0f);
        if(action!= null)
        {
            action(this);
        }
        

    }

    /// <summary>
    /// Initilizacion of the component, this is a method from MonoBehaviour
    /// </summary>
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, 3);

    }

    /// <summary>
    /// Is called once per frame, this is a method from MonoBehaviour
    /// </summary>
    void Update () {
		
	}
}
