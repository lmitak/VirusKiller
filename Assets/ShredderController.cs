using UnityEngine;
using System.Collections;

public class ShredderController : MonoBehaviour {

    public GameManager manager;

	void OnTriggerEnter2D(Collider2D col)
    {
        //if object is a ball send game over message
        if (col.gameObject.GetComponent<Ball>())
        {
            manager.BallLost();
        }
        //else destroy the object
        else
        {
            Destroy(col.gameObject);
        }
        
    }
}
