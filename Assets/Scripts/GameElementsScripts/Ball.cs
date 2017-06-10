using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float initialSpeed;
    public float speedAccelerationOverTime = 0.01f;
    public Paddle paddle;
    public AudioSource popSound;
    [Tooltip("Offset for preventing bouncing lock between paddle and edge")]
    public float collisionOffset = .25f;

    private Rigidbody2D ballRB;
    private float ballDiameter;
    private float lastPaddlePosX;
    private float halfPaddleLength;
    private float speed;

    //states
    private bool ballInGame;
    private bool isBallOnPaddle;

    

    // Use this for initialization
    void Start () {
        ballInGame = false;
        isBallOnPaddle = true;
        ballRB = GetComponent<Rigidbody2D>();
        ballDiameter = GetComponent<SpriteRenderer>().bounds.size.y;
        speed = initialSpeed;
        
        lastPaddlePosX = paddle.transform.position.x;
        halfPaddleLength = paddle.GetComponent<SpriteRenderer>().bounds.extents.x;
        ResetBallOnPaddle();
	}

	
	void FixedUpdate () {

        if(!isBallOnPaddle)
        {
            /// Keep the ball on constant speed
            //ballRB.velocity = initialSpeed * ballRB.velocity.normalized;
            /// Accelerate the ball
            speed += speedAccelerationOverTime * Time.deltaTime;
            ballRB.velocity = speed * ballRB.velocity.normalized;
        }

        //PC version input
        if (Input.GetKeyDown(KeyCode.UpArrow) && isBallOnPaddle)
        {
            SetBallOnPaddle(false);
            ballRB.velocity = new Vector2(0f, initialSpeed);
            if (paddle.GetBuff() == Paddle.Buffs.Sticky)
            {
                paddle.RemoveStickyPaddle();
            }
        }

        //Mobile version input
        if ( (Input.touchCount > 0) && isBallOnPaddle)
        {
            SetBallOnPaddle(false);
            ballRB.velocity = new Vector2(0f, initialSpeed);
            if (paddle.GetBuff() == Paddle.Buffs.Sticky)
            {
                paddle.RemoveStickyPaddle();
            }

        }

        /// If ball is placed on the paddle, move it along with paddle when paddle is moved
        if (isBallOnPaddle)       
        {
            KeepBallOnPaddle();
        }
        lastPaddlePosX = paddle.transform.position.x;
    }
    
    /**Resets ball on the middle of the paddle**/
    public void ResetBallOnPaddle()
    {
        PlaceBallOnPaddle(paddle.transform.position.x);
    }

    /// <summary>
    /// Places Ball object of positionX on the paddle
    /// positionX cannot be out of bounds of the paddle
    /// </summary>
    /// <param name="positionX">x coordinate of ball</param>
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
        if(state)
        {
            ballRB.velocity = new Vector2();
        }
        ballRB.isKinematic = state;
    }

    public void SetBallOnPaddle(bool state)
    {
        isBallOnPaddle = state;
        SetBallKinemtatic(state);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /**If ball is on paddle don't detect collision**/
        if (!isBallOnPaddle)
        {
            if (collision.gameObject == paddle.gameObject)
            {
                float ballPositionX = gameObject.transform.position.x;
                ///check hit offset from paddle-s center in percentage
                float hitOffset = Mathf.Abs(ballPositionX) / (Mathf.Abs(lastPaddlePosX) + (Mathf.Abs(halfPaddleLength)));
                ///hitOffset = Mathf.Clamp(hitOffset, 0f, 1f);
                ///Check if the ball has hit paddle on the left side
                if (ballPositionX < lastPaddlePosX)
                {
                    ///If it did, then move ball to the left
                    ballRB.velocity = new Vector2(ballRB.velocity.x - hitOffset * collisionOffset, ballRB.velocity.y);
                }
                else if (gameObject.transform.position.x < lastPaddlePosX)///else check if it has hit the right side
                {
                    ///If it did, then move ball to the right
                    ballRB.velocity = new Vector2(ballRB.velocity.x + hitOffset * collisionOffset, ballRB.velocity.y);
                }
            }else
            {
                /**Add additional velocity to ball to prevent infinite loop**/
                float additionalRandomVelocityX = Random.Range(-(collisionOffset / 2), (collisionOffset / 2));
                ballRB.velocity = new Vector2(ballRB.velocity.x + additionalRandomVelocityX, ballRB.velocity.y);
            }

            /**Play sound if ball collides with anything, except viruses**/
            if (!collision.gameObject.GetComponent<Enemy>())
            {
                popSound.pitch = Random.Range(.8f, 1f);
                popSound.Play();
            }
        }

        
    }
}
