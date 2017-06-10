using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public Text lblTotalScore;
    //public Text lblLevelScore;
    public Text lblLives;
    public int newLifeThreshold = 50;
    [Tooltip("Amount to add on threshold for next new life")]
    public int addToThreshoeld = 50; 

    private PlayerData savedPlayerData;
    private int currentLives;
    private int currentLevelScore;
    private int totalScore;

// Use this for initialization
void Start () {
        savedPlayerData = DataController.GetInstance().GetPlayerData();
        currentLives = savedPlayerData.GetLifePoints();
        totalScore = savedPlayerData.GetTotalPoints();
        currentLevelScore = 0;
        
        UpdateText(lblTotalScore, totalScore);
        UpdateText(lblLives, currentLives);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int ReduceLife()
    {
        UpdateText(lblLives, --currentLives);
        return currentLives;
    }

    public void IncreaseScore(int amount)
    {
        currentLevelScore += amount;
        totalScore += amount;
        //UpdateText(lblLevelScore, currentLevelScore);
        UpdateText(lblTotalScore, totalScore);
        if(totalScore >= newLifeThreshold)
        {
            UpdateText(lblLives, ++currentLives);
        }
    }

    private void UpdateText(Text textObj, int amount)
    {
        textObj.text = textObj.text.Substring(0, textObj.text.IndexOf(" ") + 1) + amount;
    }

    public int GetCurrentLevelScore()
    {
        return currentLevelScore;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

}
