using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour {

    public Text textLives, textScore;
    public int lives = 3;

	// Use this for initialization
	void Start () {
        PlayerData data = DataController.GetInstance().playerData;

        if (data == null)
        {
            textLives.text = lives.ToString();
            textScore.text = "0";
        }
        else
        {
            textLives.text = data.totalLives.ToString();
            textScore.text = data.totalScore.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
