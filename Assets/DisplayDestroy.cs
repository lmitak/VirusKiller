using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDestroy : MonoBehaviour {


    private GameObject textDisplay;
    private float animationLength;

    void Awake()
    {
        textDisplay = transform.GetChild(0).gameObject;
        Animator animator = textDisplay.GetComponent<Animator>();
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        animationLength = ac.animationClips[0].length;
    }

    void OnEnable()
    {
        Invoke("Destroy", animationLength);
    }

	void Destroy()
    {
        gameObject.SetActive(false);
    } 
}
