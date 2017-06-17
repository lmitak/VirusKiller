using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetStats : MonoBehaviour {

    private Button self;
    private DataController dataController;

    void Awake()
    {
        dataController = GameObject.FindObjectOfType<DataController>();
    }

	// Use this for initialization
	void Start () {
        self = this.GetComponent<Button>();
        if (dataController != null)
            self.onClick.AddListener(ResetPlayerData);
	}
	
	private void ResetPlayerData()
    {
        dataController.playerData = PlayerData.NewPlayer(dataController.startingLifePoints);
        dataController.Save();
    }


}
