using UnityEngine;
using System.Collections;

public class TransitionBall : MonoBehaviour {

    public Sprite virusBall;
    public Sprite antibacterialBall;
    public Vector2 forceApplied;
    public ParticleSystem psVirusExplosion;
    public ParticleSystem psBlueExplosion;
    public LevelStatsDisplay levelStatsDisplay;

    private Rigidbody2D rBody;
    private SpriteRenderer spriteRenderer;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private int chosenLevel;

    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void KickVirusBall(int level)
    {
        chosenLevel = level;
        spriteRenderer.sprite = virusBall;
        KickBall();
    }

    public void KickAntibacterialBall()
    {
        chosenLevel = -1;
        spriteRenderer.sprite = antibacterialBall;
        KickBall();
    }

    public void KickBall()
    {
        gameObject.transform.position = startingPosition;
        gameObject.transform.rotation = startingRotation;
        rBody.isKinematic = false;
        rBody.AddForce(forceApplied, ForceMode2D.Force);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {

        if(chosenLevel == -1)
        {
            psBlueExplosion.Play();
            levelStatsDisplay.MoveToBack();
        }else
        {
            psVirusExplosion.Play();
            levelStatsDisplay.DisplayPanel(chosenLevel);
        }
        
    }


}
