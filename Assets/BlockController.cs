﻿using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public int life;
    public int points;
    public GameManager manager;
    public AudioClip gruntSound;
    
    
    public ParticleSystem virusDeathParticles;
    public AudioSource virusDeathAudio;

    private int currentLife;
    private AudioSource audioSource;
    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentLife = life;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            currentLife--;
        }

        if(currentLife <= 0)
        {
            //audioSource.clip = deathSound;
            //audioSource.Play();
            OnDeath();
            
        }else
        {
            audioSource.clip = gruntSound;
            audioSource.Play();
            Debug.Log("Life was lost and animator " + animator + "was triggered");
            animator.SetTrigger("lifeLost");
        }
    }

    private void OnDeath()
    {
        manager.IncreaseScore(points);

        virusDeathParticles.transform.parent = null;
        virusDeathParticles.Play();
        virusDeathAudio.Play();


        Destroy(virusDeathParticles.gameObject, virusDeathAudio.clip.length);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

}
