using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour {

    public Text textLives, textScore;
    public int lives = 3;

	// Use this for initialization
	void Start () {
        PlayerData data = GameObject.FindObjectOfType<DataController>().GetPlayerData();

        if (data == null)
        {
            textLives.text = lives.ToString();
            textScore.text = "0";
        }
        else
        {
            textLives.text = data.GetLifePoints().ToString();
            textScore.text = data.GetTotalPoints().ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
