using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSet : MonoBehaviour {

    public GameObject levelBtn;
    //public Vector3 startingPos = new Vector3(-2f, 3f, 0);
    public Vector3 startingPos = new Vector3(150, 1500, 0);
    public float horizontalPadding = 15f;
    public float verticalPadding = 15f;

    private PlayerData playerData;
    private LevelManager levelManager;

    private Text levelBtnText;
    private Button levelBtnButton;
    private Camera mainCamera;

    void Awake()
    {
        playerData = DataController.GetInstance().playerData;
        if(playerData == null)
        {
            playerData = new PlayerData();
        }
        
        levelManager = LevelManager.GetInstance();
    }
    
	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

        levelBtnText = levelBtn.GetComponentInChildren<Text>();
        levelBtnButton = levelBtn.GetComponentInChildren<Button>();

        float buttonWidth = levelBtnButton.GetComponent<RectTransform>().rect.width;
        float buttonHeight = levelBtnButton.GetComponent<RectTransform>().rect.height;


        if(playerData != null && playerData.achievedLevel > 0)
        {
            Vector3 newPos = new Vector3(0, 0, 0);
            for (int level = 0; (level < playerData.achievedLevel + 1) && (level < levelManager.GetLevelCount()); level++)
            {
                levelBtnText.text = (level + 1).ToString();

                if (level == 0)
                {
                    newPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
                }
                else if (level % 4 == 0)
                {
                    newPos = new Vector3(startingPos.x, newPos.y + buttonHeight + verticalPadding, 0);
                }
                else
                {
                    newPos = newPos + new Vector3(horizontalPadding + buttonWidth, 0, 0);
                }
                Instantiate(levelBtn, new Vector3(mainCamera.ScreenToWorldPoint(newPos).x, mainCamera.ScreenToWorldPoint(newPos).y), Quaternion.identity, transform);
            }
        } else
        {
            levelBtnText.text = "1";
            Vector3 worldPointPos = mainCamera.ScreenToWorldPoint(new Vector3(startingPos.x, startingPos.y));
            Instantiate(levelBtn, new Vector3(worldPointPos.x, worldPointPos.y), Quaternion.identity, transform);
        }
      
        //if (data == null)
        //{
        //    levelBtnText.text = "1";
        //    Vector3 worldPointPos = mainCamera.ScreenToWorldPoint(new Vector3(startingPos.x, startingPos.y));
        //    Instantiate(levelBtn, new Vector3(worldPointPos.x, worldPointPos.y) , Quaternion.identity, transform);
        //}
        //else
        //{
        //    int maxLevel = data.GetAchievedLevel();
        //    if (maxLevel != 0)
        //    {
        //        Vector3 newPos = new Vector3(0, 0, 0);
        //        for (int level = 0; (level < maxLevel + 1) && (level < levelManager.GetLevelCount()); level++)
        //        {
        //            levelBtnText.text = (level + 1).ToString();

        //            if (level == 0)
        //            {
        //                newPos = new Vector3(startingPos.x, startingPos.y, startingPos.z);
        //            }
        //            else if (level % 4 == 0)
        //            {
        //                newPos = new Vector3(startingPos.x, newPos.y + buttonHeight + verticalPadding, 0);
        //            }
        //            else
        //            {
        //                newPos = newPos + new Vector3(horizontalPadding + buttonWidth, 0, 0);
        //            }
        //            Instantiate(levelBtn,  new Vector3(mainCamera.ScreenToWorldPoint(newPos).x, mainCamera.ScreenToWorldPoint(newPos).y), Quaternion.identity, transform);
        //        }
        //        //levelBtnText.text = levels.Count.ToString();
        //        //Instantiate(levelBtn, levelBtn.GetComponent<Transform>().position + new Vector3(30, 0, 0), Quaternion.identity, transform);
        //    } else
        //    {
        //        levelBtnText.text = "1";
        //        Vector3 worldPointPos = mainCamera.ScreenToWorldPoint(new Vector3(startingPos.x, startingPos.y));
        //        Instantiate(levelBtn, new Vector3(worldPointPos.x, worldPointPos.y), Quaternion.identity, transform);
        //    }
        //}

    }

}
