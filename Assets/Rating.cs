using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour {

    public int oneStarRating;
    public int twoStarRating;
    public int threeStarRating;

    private static Rating rating;

    public int CalculateRating(int score)
    {
        int stars = 0;

        if (score >= threeStarRating)
        {
            stars = 3;
        }
        else if (score >= twoStarRating)
        {
            stars = 2;
        }
        else if (score >= oneStarRating)
        {
            stars = 1;
 
        }
        return stars;
    }

    public static Rating GetInstance()
    {
        return rating;
    }

    // Use this for initialization
    void Start()
    {
        if (rating == null)
        {
            rating = this;
        }
    }
}
