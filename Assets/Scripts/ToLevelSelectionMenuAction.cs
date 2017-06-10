using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToLevelSelectionMenuAction : MonoBehaviour {

    LevelManager manager;
    Button button;


	// Use this for initialization
	void Awake () {
        manager = GameObject.FindObjectOfType<LevelManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
	}

    void TaskOnClick()
    {
        manager.LoadLevelSelectionScene();
    }
	
	
}
