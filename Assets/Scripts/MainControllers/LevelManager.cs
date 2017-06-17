using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public List<int> levelsIndex;
    private static LevelManager lmInstance;

    void Awake()
    {
        if(lmInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            lmInstance = this;
        }else if(lmInstance != this)
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

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    LoadPreviousLevel();
        //}
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool NextLevelExists()
    {
        return SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1;
    }

    /**What happens when player clicks 'Back' button**/
    public void LoadPreviousLevel()
    {
        ///If player is in game, return him to the 'LevelSelectionScene'
        if (PlayerInGame())
        {
            LoadLevelSelectionScene();
        }

        ///if 'Back' is not clicked on first scene, return him to the logo
        if(SceneManager.GetActiveScene().buildIndex < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else ///else quit the game
        {
            Application.Quit();
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int level)
    {
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
        return levelsIndex.IndexOf(SceneManager.GetActiveScene().buildIndex) + 1;
    }

    public int GetLevelCount()
    {
        return levelsIndex.Count;
    }

    public static LevelManager GetInstance()
    {
        return lmInstance;
    }

}


