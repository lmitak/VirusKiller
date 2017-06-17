using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    //for panel movement (pc)
    public float movementStep;
    //for panel movement (phone)
    public float movementFactor = 15.0f;
    public Sprite stickyPaddleSprite;
    public float accelerationThreshold = .15f;
    public ScoreSystem scoreSystem;
    public Inventory inventory;

    public enum Buffs
    {
        None,
        Sticky
    }

    private float maxWidth;

    private Sprite defaultSprite;
    private SpriteRenderer spriteRenderer;
    private Buffs buff;

	// Use this for initialization
	void Start () {
        Vector3 upperRightCorner = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperRightCorner);
        spriteRenderer = GetComponent<SpriteRenderer>();
        float paddleWidth = spriteRenderer.bounds.extents.x;
        maxWidth = targetWidth.x - paddleWidth;

        
        defaultSprite = spriteRenderer.sprite;
	}


    void FixedUpdate() {

        float movementX;

        //PC version input
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementX = Mathf.Clamp(transform.position.x - movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementX = Mathf.Clamp(transform.position.x + movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }



        //Mobile version input
        //if( Input.acceleration.x < (-accelerationThreshold))
        //{
        //    float movementX = Mathf.Clamp(transform.position.x - movementStep * Time.deltaTime, -maxWidth, maxWidth);
        //    transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        //}
        //if( Input.acceleration.x > accelerationThreshold)
        //{
        //    float movementX = Mathf.Clamp(transform.position.x + movementStep * Time.deltaTime, -maxWidth, maxWidth);
        //    transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        //}
        if (Input.acceleration.magnitude > 0)
        {
            movementStep = Mathf.Sign(Input.acceleration.x) * Mathf.Pow(Input.acceleration.x, 2f) * movementFactor;
            
            movementX = Mathf.Clamp(transform.position.x + movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            scoreSystem.ComboBreak();
            if(buff == Buffs.Sticky)
            {
                Ball ball = collision.gameObject.GetComponent<Ball>();
                ball.PlaceBallOnPaddle(ball.transform.position.x);
            }
        } 
        
        //if (ball != null && buff == Buffs.Sticky)
        //{
        //    ball.PlaceBallOnPaddle(ball.transform.position.x);          
        //} else
        //{
        //    scoreSystem.ComboBreak();
        //}
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Item")
        {
            Item item = collider.GetComponent<Item>();
            Debug.Log(item.GetItemType());
            if (item.GetItemType() == ItemType.PaddleRelated) 
            {
                ((ItemPaddle)item).paddle = this;
            }
            Debug.Log(((ItemPaddle)item).paddle);
            inventory.AddItem(item);
        }

    }

    public void ApplyStickyPaddle()
    {
        buff = Buffs.Sticky;
        spriteRenderer.sprite = stickyPaddleSprite;
    }

    public void RemoveStickyPaddle()
    {
        buff = Buffs.None;
        spriteRenderer.sprite = defaultSprite;
    }

    public Buffs GetBuff()
    {
        return buff;
    }
}
