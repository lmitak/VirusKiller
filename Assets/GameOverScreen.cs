using System.Collections;
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
        /// Check if currentLevel was already achieved and if true, check if old highscore is higher than the newScore
        if(playerData.achievedLevel > currentLevel && playerData.GetPlayerStatsForLevel(currentLevel).highscore > newScore)
        {
            /// Display old highscore
            lblBestScoreAmount.text = playerData.GetPlayerStatsForLevel(currentLevel).highscore.ToString();
            /// Display how many stars player has achieved
            displayRating.SetRating(playerData.GetPlayerStatsForLevel(currentLevel).ratingStars);
        }
        else
        {
            /// Display new highscore
            lblBestScoreAmount.text = newScore.ToString();
            /// Calculate and display how many stars has player achieved
            displayRating.SetRating(Rating.GetInstance().CalculateRating(newScore));
        }
    }

    /// <summary>
    /// Display hidden UI objects
    /// </summary>
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

