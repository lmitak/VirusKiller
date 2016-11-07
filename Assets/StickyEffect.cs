using UnityEngine;
using System.Collections;

public class StickyEffect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Paddle>())
        {
            col.gameObject.GetComponent<Paddle>().ApplyStickyPaddle();
            Destroy(gameObject);
        }

    }
   
}
