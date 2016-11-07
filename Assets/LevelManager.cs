using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public List<int> levelsIndex;
    public static LevelManager lManager;

    void Awake()
    {
        if(lManager == null)
        {
            DontDestroyOnLoad(gameObject);
            lManager = this;
        }else if(lManager != this)
        {
            Destroy(gameObject);
        }

        levelsIndex = new List<int>();
        /*Lists of all levels in level-buildIndex fashion*/
        levelsIndex.Add(2);
        levelsIndex.Add(3);
    }

    void Start()
    {
        
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool NextLevelExists()
    {
        return SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int level)
    {
        Debug.Log("called; " + (level - 1) + "should be loaded,");
        Debug.Log("sceneIndex: " + (levelsIndex[level - 1]));
        SceneManager.LoadScene(levelsIndex[level - 1]);
    }

    public void LoadLevelSelectionScene()
    {
        SceneManager.LoadScene(1);
    }

    //Check if player is in game or in menu
    public bool PlayerInGame()
    {
        if (SceneManager.GetActiveScene().name.IndexOf("Level") == -1)
        {
            return false;
        }

        return true;
    }

    public int GetCurrentLevelNumber()
    {
        return levelsIndex.IndexOf(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


