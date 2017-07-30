using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text lblTotalScore;
    //public Text lblLevelScore;
    public Text lblLives;
    public Text lblCombo;
    public int newLifeThreshold = 50;
    [Tooltip("Amount to add on threshold for next new life")]
    public int addToThreshold = 50; 

    private PlayerData savedPlayerData;
    private int currentLives;
    private int currentLevelScore;
    private int totalScore;
    private int livesGain;
    private int baseLifeThreshold;

    private int comboMultiplier;
    private int comboScoreStore;

    // Use this for initialization
    void Start ()
    {
        savedPlayerData = DataController.GetInstance().playerData;
        currentLives = savedPlayerData.totalLives;
        totalScore = savedPlayerData.totalScore;
        livesGain = savedPlayerData.livesGain;
        currentLevelScore = 0;

        baseLifeThreshold = newLifeThreshold;
        newLifeThreshold = GetNextLifeThreshold();

        UpdateText(lblTotalScore, totalScore);
        UpdateText(lblLives, currentLives);

        this.ComboBreak();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private int GetNextLifeThreshold()
    {
        int lastThreshold = 0;
        for(int i = 0; i <= livesGain; i++)
        {
            lastThreshold += addToThreshold * i + baseLifeThreshold; 
        }
        return lastThreshold;
    }

    public int ReduceLife()
    {
        ComboBreak();
        UpdateText(lblLives, --currentLives);
        return currentLives;
    }

    
    public void IncreaseScore(int amount)
    {
        comboMultiplier++;
        comboScoreStore += amount;
        lblCombo.enabled = true;
        string displayedScore = "+" + comboScoreStore;
        if(comboMultiplier > 1)
        {
            displayedScore += " x" + comboMultiplier;
        }
        lblCombo.text = displayedScore;
    }

    private void SaveStoredScore()
    {
        currentLevelScore += comboMultiplier * comboScoreStore;
        totalScore += comboMultiplier * comboScoreStore;
        UpdateText(lblTotalScore, totalScore);
        if (totalScore >= newLifeThreshold)
        {
            UpdateText(lblLives, ++currentLives);
            newLifeThreshold += addToThreshold * ++livesGain + baseLifeThreshold;
        }
    }

    public void ComboBreak()
    {
        lblCombo.enabled = false;
        SaveStoredScore();
        comboMultiplier = 0;
        comboScoreStore = 0;
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

    public PlayerData GetNewPlayerData()
    {
        return new PlayerData(currentLives, totalScore, livesGain);
    }
}
