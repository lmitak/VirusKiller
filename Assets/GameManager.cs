using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    public Text textTitle;
    public GameObject btnRetry;
    public GameObject btnNextLevel;
    public GameObject btnBack;
    public Ball ball;
    public Text textTotalScore;
    public Text textLevelScore;
    public Text textLife;
    public Text textBestScore;
    public int playerLives = 3;

    private int playerTotalScore;
    private int playerLevelScore;
    private int currentLevel;
    private bool gameIsWon;

    private LevelManager levelManager;
    private DataController dataObj;
    private PlayerData playerData = null;

    private static string LOSE = "GAME OVER";
    private static string WIN = "LEVEL COMPLETED!";
    //private static string FILE_NAME = "playerInfo.dat";

    void Awake()
    {
        dataObj = GameObject.FindObjectOfType<DataController>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        playerData = dataObj.GetPlayerData();

        //UpdateText(textScore, playerScore);
        //UpdateText(textLife, playerLives);
    }

    // Use this for initialization
    void Start()
    {
        currentLevel = levelManager.GetCurrentLevelNumber();

        if (playerData != null)
        {
            playerLives = playerData.GetLifePoints();
            playerTotalScore = playerData.GetTotalPoints();
        }
        else
        {
            playerData = new PlayerData();
            playerData.SetLifePoints(playerLives);
            playerData.SetTotalPoints(0);
            playerTotalScore = 0;
        }

        playerLevelScore = 0;
        gameIsWon = false;

        UpdateText(textTotalScore, playerTotalScore);
        UpdateText(textLife, playerLives);
        UpdateText(textLevelScore, playerLevelScore);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("scoreAmount: " + playerScore);
    }

    public void IncreaseScore(int amount)
    {
        playerTotalScore = playerTotalScore + amount;
        playerLevelScore += amount;
        
        UpdateText(textTotalScore, playerTotalScore);
        UpdateText(textLevelScore, playerLevelScore);
    }


    public void BallLost()
    {
        playerLives--;
        if (playerLives < 0)
        {
            playerLives = 0;
        }
        UpdateText(textLife, playerLives);

        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            ball.ResetBallOnPaddle();
        }
    }

    public void GameOver()
    {
        textTitle.text = LOSE;
        textTitle.gameObject.SetActive(true);

        btnBack.SetActive(true);
        btnRetry.SetActive(true);

        ball.SetBallKinemtatic(true);
    }

    /**Display 'Level Completed' title and appropriate menu options**/
    public void GameWon()
    {
        gameIsWon = true;
        textTitle.text = WIN;
        textTitle.gameObject.SetActive(true);

        /**Check if the player has already won this level before**/
        if (playerData.GetAchievedLevel() > currentLevel)
        {
            int oldLevelScore = playerData.GetPointsOfLevel(currentLevel);
            /**If his new score is better then the last one, display it as new best score**/
            if(oldLevelScore < playerLevelScore)
            {
                textBestScore.text = playerLevelScore.ToString();
            }else
            {
                textBestScore.text = oldLevelScore.ToString();
            }
        }else
        {
            textBestScore.text = playerLevelScore.ToString();
        }


        btnNextLevel.SetActive(true);
        btnRetry.SetActive(true);
        /**Change position of Retry button when game is won**/
        btnRetry.GetComponent<RectTransform>().position = new Vector3(
            btnRetry.GetComponent<RectTransform>().position.x, 
            btnNextLevel.GetComponent<RectTransform>().position.y - 300f);
        btnBack.SetActive(true);

        ball.SetBallKinemtatic(true);
    }

    //Save game progress
    public void GameSave()
    {
        playerData.SetLivesLostAndScoreOfLevel(currentLevel,
            playerData.GetLifePoints() - playerLives,
            playerTotalScore - playerData.GetTotalPoints());
        playerData.SetLifePoints(playerLives);
        playerData.SetTotalPoints(playerTotalScore);

        dataObj.SetPlayerData(playerData);
        dataObj.Save();
    }

    /**OnClick actions for 'Next level' and 'Back' buttons**/

    public void BtnNextLevelAction()
    {
        /**Check if the player has already won this level before**/
        if (playerData.GetAchievedLevel() > currentLevel)
        {
            int oldLevelScore = playerData.GetPointsOfLevel(currentLevel);
            /**If his new score is better then the last one, save it**/
            if (oldLevelScore < playerLevelScore)
            {
                GameSave();
            }
        }
        else //if he didn't, save new score
        {
            GameSave();
        }
        levelManager.LoadNextLevel();
    }

    public void BtnBackAction()
    {
        /**If game is won, save data before leaving current level**/
        if (gameIsWon)
        {
            /**Check if the player has already won this level before**/
            if (playerData.GetAchievedLevel() > currentLevel)
            {
                int oldLevelScore = playerData.GetPointsOfLevel(currentLevel);
                /**If his new score is better then the last one, save it**/
                if (oldLevelScore < playerLevelScore)
                {
                    GameSave();
                }
            }
            else //if he didn't, save new score
            {
                GameSave();
            }
        }
        
        levelManager.LoadLevelSelectionScene();
    }


    private void UpdateText(Text textObj, int amount)
    {
        textObj.text = textObj.text.Substring(0, textObj.text.IndexOf(" ") + 1) + amount;
    }

    public List<int> GetPlayerLevels()
    {
        return playerData.GetLevelPoints();
    }
}