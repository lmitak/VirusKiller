﻿using UnityEngine;
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

    private int playerTotalScore;
    private int playerLevelScore;
    
    private bool isGameWon;
    private int currentLevel;

    private LevelManager levelManager;
    private DataController dataController;
    private PlayerData playerData = null;

    // Use this for initialization
    void Start()
    {
        levelManager = LevelManager.GetInstance();
        currentLevel = levelManager.GetCurrentLevelNumber();
        dataController = DataController.GetInstance();
        playerData = dataController.GetPlayerData();

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
        isGameWon = false;
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("scoreAmount: " + playerScore);

        ///When player uses back button on phone, return him to level selection scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.LoadLevelSelectionScene();
        }
    }

    public void BallLost()
    {
        int lives = scoreSystem.ReduceLife();

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
        playerTotalScore = scoreSystem.GetTotalScore();
        playerLevelScore = scoreSystem.GetCurrentLevelScore();
        ball.SetBallKinemtatic(true);
        gameOverScreen.ShowWinScreen(playerTotalScore);
    }

    //Save game progress
    public void GameSave()
    {
        playerData.SetLivesLostAndScoreOfLevel(currentLevel,
            playerData.GetLifePoints() - playerLives,
            playerTotalScore - playerData.GetTotalPoints());
        playerData.SetLifePoints(playerLives);
        playerData.SetTotalPoints(playerTotalScore);

        dataController.SetPlayerData(playerData);
        dataController.Save();
    }

    /**OnClick actions for 'Next level' and 'Back' buttons**/
    public void BtnNextLevelAction()
    {
        /// If current level is higher than the last achieved level or new score of current level is higher than the old one, save game 
        if ((playerData.GetAchievedLevel() < currentLevel) || (playerData.GetPointsOfLevel(currentLevel) < playerLevelScore))
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
            if( (playerData.GetAchievedLevel() < currentLevel) || (playerData.GetPointsOfLevel(currentLevel) < playerLevelScore) )
            {
                GameSave();
            }
        }

        levelManager.LoadLevelSelectionScene();
    }


    public List<int> GetPlayerLevels()
    {
        return playerData.GetLevelPoints();
    }
}