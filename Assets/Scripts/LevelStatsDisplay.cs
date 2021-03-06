﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStatsDisplay : MonoBehaviour {

    public Text textBestScore;
    public Button buttonPlay;

    private LevelManager manager;
    private PlayerData data;

    private int level;

    void Awake()
    {
        manager = LevelManager.GetInstance();
        data = DataController.GetInstance().playerData;
    }

	// Use this for initialization
	void Start () {
        //gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void DisplayPanel(int level)
    {
        this.level = level;
        if (data == null)
        {
            textBestScore.text = "0";
        }else
        {
            if(data.achievedLevel < level)
            {
                textBestScore.text = "0";
            }else
            {
                int score = data.GetPlayerStatsForLevel(level).highscore;
                textBestScore.text = score.ToString();
            }
        }

        buttonPlay.onClick.AddListener(OnPlay);
        MoveToFront();
    }

    public void OnPlay()
    {   
        manager.LoadLevel(level);
    }

    public void MoveToFront()
    {
        transform.SetAsLastSibling();
    }

    public void MoveToBack()
    {
        transform.SetAsFirstSibling();
    }

    public bool isLastSibling()
    {
        return transform.GetSiblingIndex() == (transform.parent.childCount - 1);
    }

}
