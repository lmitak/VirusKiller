using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public ScoreSystem scoreSystem;
    public GameOverScreen gameOverScreen;

    public int playerLives = 3;

    private int playerLevelScore;
    
    private bool isGameWon;
    private int currentLevel;

    private LevelManager levelManager;
    private DataController dataController;
    private PlayerData playerData = null;
    private PlayerData newPlayerData = null;

    // Use this for initialization
    void Start()
    {
        levelManager = LevelManager.GetInstance();
        currentLevel = levelManager.GetCurrentLevelNumber();
        dataController = DataController.GetInstance();
        playerData = dataController.playerData;

        if (playerData != null)
        {
            playerLives = playerData.totalLives;
            
        }
        else
        {
            playerLives = DataController.GetInstance().startingLifePoints;
            playerData = PlayerData.NewPlayer(playerLives);
        }
        isGameWon = false;
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ///When player uses back button on phone, return him to level selection scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.LoadLevelSelectionScene();
        }
    }

    public void BallLost()
    {
        int lives = scoreSystem.ReduceLife();
        scoreSystem.ComboBreak();

        if (lives <= 0)
        {
            ball.SetBallKinemtatic(true);
            gameOverScreen.ShowLoseScreen();
        }
        else
        {
            ball.ResetBallOnPaddle();
        }
    }

    public void GameWon()
    {
        isGameWon = true;
        scoreSystem.ComboBreak();
        newPlayerData = scoreSystem.GetNewPlayerData();
        playerLevelScore = scoreSystem.GetCurrentLevelScore();
        ball.SetBallKinemtatic(true);
        gameOverScreen.ShowWinScreen(newPlayerData.totalScore);
    }

    /// <summary>
    /// Save game progress
    /// </summary>
    public void GameSave()
    {
        PlayerLevelStats levelStats = new PlayerLevelStats(currentLevel, playerLevelScore);
        levelStats.ratingStars = Rating.GetInstance().CalculateRating(playerLevelScore);
        playerData.AddStatsForNewLevel(levelStats);
        playerData.totalScore = newPlayerData.totalScore;
        playerData.totalLives = newPlayerData.totalLives;
        playerData.livesGain = newPlayerData.livesGain;

        dataController.playerData = playerData;
        dataController.Save();
    }

    /**OnClick actions for 'Next level' and 'Back' buttons**/
    public void BtnNextLevelAction()
    {
        /// If current level is higher than the last achieved level or new score of current level is higher than the old one, save game 
        if ((playerData.achievedLevel < currentLevel) || (playerData.GetPlayerStatsForLevel(currentLevel).highscore < playerLevelScore))
        {
            GameSave();
        }
        levelManager.LoadNextLevel();
    }

    public void BtnBackAction()
    {
        if (isGameWon)
        {
            /// If current level is higher than the last achieved level or new score of current level is higher than the old one, save game 
            if( (playerData.achievedLevel < currentLevel) || (playerData.GetPlayerStatsForLevel(currentLevel).highscore < playerLevelScore) )
            {
                GameSave();
            }
        }

        levelManager.LoadLevelSelectionScene();
    }


    //public List<int> GetPlayerLevels()
    //{
    //    return playerData.GetLevelPoints();
    //}
}