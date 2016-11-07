using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSet : MonoBehaviour {

    public GameObject levelBtn;
    //public Vector3 startingPos = new Vector3(-405, 707, 0);
    public Vector3 startingPos = new Vector3(100, 1600, 0);
    public float horizontalPadding = 15f;
    public float verticalPadding = 15f;

    private PlayerData data;
    private LevelManager levelManager;

    private Text levelBtnText;
    private Button levelBtnButton; 

    void Awake()
    {
        data = GameObject.FindObjectOfType<DataController>().GetPlayerData();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
    
	// Use this for initialization
	void Start () {
        levelBtnText = levelBtn.GetComponentInChildren<Text>();
        levelBtnButton = levelBtn.GetComponentInChildren<Button>();

        if(data == null)
        {
            levelBtnText.text = "1";
            Instantiate(levelBtn, startingPos, Quaternion.identity, transform);
        }else
        {
            int maxLevel = data.GetAchievedLevel();
            Debug.Log(maxLevel);
            if (maxLevel != 0)
            {
                Vector3 newPos = new Vector3(0, 0, 0);
                for (int level = 0; level < maxLevel+1; level++)
                {
                    levelBtnText.text = (level + 1).ToString();
                    
                    if (level == 0)
                    {
                        newPos = startingPos;
                    }
                    else if (level % 4 == 0)
                    {
                        newPos = new Vector3(startingPos.x, newPos.y + 125 + verticalPadding, 0);
                    }
                    else
                    {
                        newPos = newPos + new Vector3(horizontalPadding + 139, 0, 0);
                    }
                    Instantiate(levelBtn, newPos, Quaternion.identity, transform);
                }
                //levelBtnText.text = levels.Count.ToString();
                //Instantiate(levelBtn, levelBtn.GetComponent<Transform>().position + new Vector3(30, 0, 0), Quaternion.identity, transform);
            }
        }

    }

}
