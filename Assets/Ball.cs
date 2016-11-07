using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float initialForce;
    public Paddle paddle;
    public AudioSource popSound;
    [Tooltip("Offset for preventing bouncing lock between paddle and edge")]
    public float collisionOffset = .25f;
    
    private Rigidbody2D ballRB;
    private float ballDiameter;
    private float lastPaddlePosX;

    //states
    private bool ballInGame;
    private bool ballOnPaddle;

    // Use this for initialization
    void Start () {
        ballInGame = false;
        ballOnPaddle = true;
        ballRB = GetComponent<Rigidbody2D>();
        ballDiameter = GetComponent<SpriteRenderer>().bounds.size.y;

        
        lastPaddlePosX = paddle.transform.position.x;
        ResetBallOnPaddle();
	}
	
	void FixedUpdate () {

        //PC version input
        if (Input.GetKeyDown(KeyCode.UpArrow) && ballOnPaddle)
        {
            SetBallOnPaddle(false);
            ballRB.velocity = new Vector2(0f, initialForce);
            if (paddle.GetBuff() == Paddle.Buffs.Sticky)
            {
                paddle.RemoveStickyPaddle();
            }

        }

        //Mobile version input
        if ( (Input.touchCount > 0) && ballOnPaddle)
        {
            SetBallOnPaddle(false);
            ballRB.velocity = new Vector2(0f, initialForce);
            if (paddle.GetBuff() == Paddle.Buffs.Sticky)
            {
                paddle.RemoveStickyPaddle();
            }

        }


        if (ballOnPaddle)       //If ball is placed on the paddle, move it along with paddle when paddle is moved
        {
            KeepBallOnPaddle();
        }
        lastPaddlePosX = paddle.transform.position.x;     
    }

    
    
    /**Resets ball on the middle of the paddle**/
    public void ResetBallOnPaddle()
    {
        transform.position = new Vector3(paddle.transform.position.x, 
            paddle.transform.position.y + ballDiameter, 
            paddle.transform.position.z);
        SetBallOnPaddle(true);
    }

    public void PlaceBallOnPaddle(float positionX)
    {
        transform.position = new Vector3(
            Mathf.Clamp(positionX, paddle.GetComponent<SpriteRenderer>().bounds.min.x, paddle.GetComponent<SpriteRenderer>().bounds.max.x),
            paddle.transform.position.y + ballDiameter,
            paddle.transform.position.z);
        SetBallOnPaddle(true);
    }

    /**Calculates diffrence between current nad last frame paddle x position and adds that to ball x position**/  
    private void KeepBallOnPaddle()
    {
        float distance = paddle.transform.position.x - lastPaddlePosX;
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
    }

    public void SetBallInGame(bool statement)
    {
        ballInGame = statement;
    }

    public void SetBallKinemtatic(bool state)
    {
        ballRB.isKinematic = state;
    }

    public void SetBallOnPaddle(bool state)
    {
        ballOnPaddle = state;
        SetBallKinemtatic(state);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float additionalRandomVelocityX = Random.Range(-collisionOffset, collisionOffset);
        ballRB.velocity = new Vector2(ballRB.velocity.x + additionalRandomVelocityX, ballRB.velocity.y);

        if (!collision.gameObject.GetComponent<BlockController>())
        {
            popSound.pitch = Random.Range(.8f, 1f);
            popSound.Play();
        }
    }
}
