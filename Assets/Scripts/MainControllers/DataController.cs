using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour {

    private static DataController dcInstance;

    public int startingLifePoints;
    public PlayerData playerData { get; set; }
    private static string FILE_NAME = "playerInfo.dat";

    void Awake()
    {
        if(dcInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            dcInstance = this;
        } else if (dcInstance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        Load();
        Debug.Log(playerData.ToString());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + FILE_NAME, FileMode.Create);
        
        bf.Serialize(file, playerData);
        file.Close();
    }

    /**Load playerData or set it to null if it does not exists**/
    public void Load()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/" + FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + FILE_NAME, FileMode.Open);
            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }else
        {
            playerData = PlayerData.NewPlayer(startingLifePoints);
        }
    }


    private DataController()
    { }

    public static DataController GetInstance()
    {
        return dcInstance;
    }
}

//[Serializable]
//public class PlayerDataO
//{
//    int achievedLevel;
//    int totalPoints;
//    int lifePoints;
//    List<int> levelPoints;
//    List<int> levelLivesLost;

//    public PlayerData() {
//        levelPoints = new List<int>();
//        levelLivesLost = new List<int>();
//        achievedLevel = 0;
//    }


//    public int GetAchievedLevel()
//    {
//        return achievedLevel;
//    }

//    public void SetAchievedLevel(int lvl)
//    {
//        achievedLevel = lvl;
//    }

//    public int GetTotalPoints()
//    {
//        return totalPoints;
//    }

//    public void SetTotalPoints(int points)
//    {
//        totalPoints = points;
//    }

//    public void SetPointsForLevel(int level, int points)
//    {
//        levelPoints[level] = points;
//    }

//    public int GetPointsOfLevel(int level)
//    {
//        return levelPoints[level-1];
//    }

//    public void SetLevelPoints(List<int> levelPoints)
//    {
//        this.levelPoints = levelPoints;
//    }

//    public List<int> GetLevelPoints()
//    {
//        return levelPoints;
//    }

//    public void SetLifePoints(int lifePoints)
//    {
//        this.lifePoints = lifePoints;
//    }
//    public int GetLifePoints()
//    {
//        return lifePoints;
//    }

//    public List<int> GetLevelLivesLost()
//    {
//        return levelLivesLost;
//    }

//    public void SetLevelLivesLost(List<int> levelLivesLost)
//    {
//        this.levelLivesLost = levelLivesLost;
//    }

//    public int GetLivesLostForLevel(int level)
//    {
//        return levelLivesLost[level - 1];
//    }

//    public void SetLivesLostForLevel(int level, int livesLost)
//    {
//        levelLivesLost[level] = livesLost;
//    }

//    public void SetLivesLostAndScoreOfLevel(int level, int livesLost, int score)
//    {
//        if(achievedLevel < level)
//        {
//            Debug.Log("DataController: " + "new data added for new level " + level);
//            this.SetAchievedLevel(level);
//            levelLivesLost.Add(livesLost);
//            levelPoints.Add(score);
//        }
//        else
//        {
//            Debug.Log("DataController: " + "new data added for old level " + level);
//            levelLivesLost[level-1] = livesLost;
//            levelPoints[level-1] = score;
//        }
//    }

//    override public string ToString()
//    {
//        return "Max level: " + achievedLevel
//            + "\nLives: " + lifePoints
//            + "\nScore: " + totalPoints;
//    }
//}
