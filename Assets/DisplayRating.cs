using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRating : MonoBehaviour {

    public Sprite goldenStarImage;
    public Sprite emptyStarImage;

    /// <summary>
    /// List of all child elements
    /// </summary>
    List<GameObject> stars;

    void Awake()
    {
        stars = new List<GameObject>();
        /// Get all child elements and add them to the list
        foreach (Transform child in this.transform)
        {
            stars.Add(child.gameObject);
        }
    }
	
    /// <summary>
    /// Set image for all 3 stars depending on parameter
    /// </summary>
    /// <param name="goldenStars">Integer that is number of stars that player achieved</param>
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
                stars[i].GetComponent<Image>().sprite = emptyStarImage;
            }
            i++;
        }
    }

}
