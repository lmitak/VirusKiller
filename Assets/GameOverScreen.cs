﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

    public Text lblTitle;
    public Text lblBestScoreText;
    public Text lblBestScoreAmount;
    public GameObject btnRetry;
    public GameObject btnNext;
    public GameObject btnBack;
    public DisplayRating displayRating;

    public string loseText = "GAME OVER";
    public string winText = "LEVEL COMPLETED!";

    private int currentLevel;
    private PlayerData playerData;

    // Use this for initialization
    void Start () {
        currentLevel = LevelManager.GetInstance().GetCurrentLevelNumber();
        playerData = DataController.GetInstance().playerData;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /**Display 'Level Completed' title and appropriate menu options**/
    public void ShowWinScreen(int newScore)
    {
        this.DisplayHiddenUIObjects();
        lblTitle.text = winText;

        /**Check if the player has already won this level before**/
        if (playerData.achievedLevel > currentLevel)
        {
            PlayerLevelStats levelStats = playerData.GetPlayerStatsForLevel(currentLevel);
            int oldHighscore = levelStats.highscore;
            /**If his new score is better then the last one, display it as new best score**/
            if (oldHighscore < newScore)
            {
                lblBestScoreAmount.text = newScore.ToString();
                displayRating.SetRating(Rating.GetInstance().CalculateRating(newScore));
            }
            else
            {
                lblBestScoreAmount.text = oldHighscore.ToString();
                displayRating.SetRating(levelStats.ratingStars);
            }
        }
        else
        {
            lblBestScoreAmount.text = newScore.ToString();
            displayRating.SetRating(Rating.GetInstance().CalculateRating(newScore));
        }

        
    }

    /**Display hidden UI objects**/
    private void DisplayHiddenUIObjects()
    {
        lblTitle.gameObject.SetActive(true);
        lblBestScoreText.gameObject.SetActive(true);
        lblBestScoreAmount.gameObject.SetActive(true);
        btnNext.SetActive(true);
        btnRetry.SetActive(true);
        /**Change position of Retry button when game is won**/
        btnRetry.GetComponent<RectTransform>().position = new Vector3(
            btnRetry.GetComponent<RectTransform>().position.x,
            btnNext.GetComponent<RectTransform>().position.y - 150f);
        btnBack.SetActive(true);
        displayRating.gameObject.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        lblTitle.text = loseText;
        lblTitle.gameObject.SetActive(true);
        btnBack.SetActive(true);
        btnRetry.SetActive(true);
    }


}

