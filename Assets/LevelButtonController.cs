using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour {

    private Button button;
    private Text text;
    private LevelManager levelManager;
    public GameObject downPanel;
    public LevelStatsDisplay levelStatsDisplay;

    private TransitionBall transitionBall;
    private Animator animator;

    void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        levelStatsDisplay = GameObject.FindObjectOfType<LevelStatsDisplay>();
        transitionBall = GameObject.FindObjectOfType<TransitionBall>();
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
        downPanel.SetActive(false);

        if (transitionBall.GetComponent<Rigidbody2D>().isKinematic)
        {
            transitionBall.KickVirusBall(int.Parse(text.text));
        }
    }

}
