using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour {

    public int oneStarRating;
    public int twoStarRating;
    public int threeStarRating;

    private static Rating rating;

	// Use this for initialization
	void Start () {
        rating = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CalculateRating(int score)
    {
        int rate = 0;

        if (rate >= threeStarRating)
        {
            rate = 3;
        }
        else if (rate >= twoStarRating)
        {
            rate = 2;
        }
        else if (rate >= oneStarRating)
        {
            rate = 1;
        }

        return rate;
    }

    public static Rating GetInstance()
    {
        return rating;
    }
}
