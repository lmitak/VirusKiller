using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float movementStep;
    public Sprite stickyPaddleSprite;
    public float accelerationThreshold = .15f;

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
	
	
	void FixedUpdate () {

        //PC version input
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float movementX = Mathf.Clamp(transform.position.x - movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float movementX = Mathf.Clamp(transform.position.x + movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }

        Debug.Log("Acceleration: " + Input.acceleration.x);

        //Mobile version input
        if( Input.acceleration.x < (-accelerationThreshold))
        {
            float movementX = Mathf.Clamp(transform.position.x - movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }
        if( Input.acceleration.x > accelerationThreshold)
        {
            float movementX = Mathf.Clamp(transform.position.x + movementStep * Time.deltaTime, -maxWidth, maxWidth);
            transform.position = new Vector3(movementX, transform.position.y, transform.position.z);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null && buff == Buffs.Sticky)
        {
            ball.PlaceBallOnPaddle(ball.transform.position.x);          
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
