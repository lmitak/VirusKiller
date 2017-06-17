using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRating : MonoBehaviour {

    
    public Sprite goldenStarImage;
    public Sprite emptyStarImage;

    List<GameObject> stars;

    void Awake()
    {
        stars = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            stars.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRating(int goldenStars)
    {
        int i = 0;
        while (i < 3)
        {
            if (goldenStars >= (i + 1))
            {
                stars[i].GetComponent<Image>().sprite = goldenStarImage;
            }
            else
            {
                stars[i].GetComponent<Image>().sprite = goldenStarImage;
            }
            i++;
        }
    }

}
