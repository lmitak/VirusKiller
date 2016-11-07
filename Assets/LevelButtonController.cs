using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour {

    private Button button;
    private Text text;
    private LevelManager levelManager;
    public GameObject downPanel;

    public LevelStatsDisplay levelStatsDisplay;

    void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        //downPanel = GameObject.FindObjectOfType<PlayerStatsDisplay>().gameObject;
        levelStatsDisplay = GameObject.FindObjectOfType<LevelStatsDisplay>();
    }

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonOnClick);

        text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ButtonOnClick()
    {
        //Debug.Log("Button " + int.Parse(text.text) + " clicked");
        //levelManager.LoadLevel(int.Parse(text.text));
        downPanel.SetActive(false);
        levelStatsDisplay.DisplayPanel(int.Parse(text.text));
    }
}
