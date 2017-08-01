using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomParticlesDestroy : MonoBehaviour {

    public float activeDuration;

    private AudioSource deathSound;
    private ParticleSystem ps;

	// Use this for initialization
	void Awake () {
        deathSound = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
        activeDuration = deathSound.clip.length > ps.main.duration ? deathSound.clip.length : ps.main.duration;
	}
	
	void OnEnable()
    {
        ps.Play();
        deathSound.Play();
        Invoke("Destroy", activeDuration);
    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
