using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class PlayerData {

	public int achievedLevel { get; set; }
    public int totalLives { get; set; }
    public int livesGain { get; set; }
    public int totalScore { get; set; }
    public List<PlayerLevelStats> levelsStats { get; set; }

    public PlayerData() { }

    public PlayerData(int achievedLevel, int totalLives, int totalScore, int livesGain)
    {
        this.achievedLevel = achievedLevel;
        this.totalScore = totalScore;
        this.totalLives = totalLives;
        this.livesGain = livesGain;
        this.levelsStats = new List<PlayerLevelStats>();
    }

    public PlayerData(int totalLives, int totalScore, int livesGain)
    {
        this.achievedLevel = -1;
        this.totalScore = totalScore;
        this.totalLives = totalLives;
        this.livesGain = livesGain;
        this.levelsStats = new List<PlayerLevelStats>();
    }

    public static PlayerData NewPlayer(int lives)
    {
        return new PlayerData(0, lives, 0, 0);
    }

    public PlayerLevelStats GetPlayerStatsForLevel(int level)
    {
        foreach(PlayerLevelStats levelStats in levelsStats)
        {
            if(levelStats.levelNumber == level)
            {
                return levelStats;
            }
        }
        return null;
    }

    public void SetPlayerStatsForLevel(int level, int highscore)
    {
        foreach (PlayerLevelStats levelStats in levelsStats)
        {
            if (levelStats.levelNumber == level)
            {
                levelStats.highscore = highscore;
            }
        }
    }

    public void AddStatsForNewLevel(PlayerLevelStats newLevelStats)
    {
        this.levelsStats.Add(newLevelStats);
        this.achievedLevel = newLevelStats.levelNumber;
    }

    override public string ToString()
    {
        return "Max level: " + achievedLevel
            + "\nLives: " + totalLives
            + "\nScore: " + totalScore;
    }

}
