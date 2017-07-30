using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEntity : MonoBehaviour {

    public int itemId;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if( (collider.GetComponent<Paddle>() || collider.GetComponent<ShredderController>()) && this.gameObject.activeInHierarchy)
        {
            this.Destroy();
        }
    }

    /// <summary>
    /// Stop object in movement and hide it
    /// </summary>
    void Destroy()
    {
        //rb.isKinematic = true;
        //rb.velocity = new Vector2();
        gameObject.SetActive(false);
    }
}
