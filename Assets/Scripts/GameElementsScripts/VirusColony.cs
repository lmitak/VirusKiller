using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VirusColony : MonoBehaviour, DeathAnnouncement {

    public GameManager manager;
    public DisplayEnemyValue enemyValueDisplay;

    private List<int> unactiveVirusesIndex;
    private bool activeVirusExists;
    private bool gameWonCalled;

	// Use this for initialization
	void Start () {
        unactiveVirusesIndex = new List<int>();
        activeVirusExists = true;
        gameWonCalled = false;
    }
	
	void Update () { }

    /// <summary>
    /// Called when enemy should die
    /// </summary>
    /// <param name="enemy">GameObject that should die</param>
    public void ImGonnaDie(Enemy enemy)
    {
        manager.EnemySlain(enemy);
        //enemyValueDisplay.ShowEnemyValue(enemy.points, enemy.transform.position);
        if(AreEnemiesDefeated())
        {
            manager.GameWon();
        }
    }

    /// <summary>
    /// Check if any child in VirusColony is active
    /// </summary>
    /// <returns></returns>
    private bool AreEnemiesDefeated()
    {
        bool activeVirusExists = false;
        // traverse through all children and check if any is active
        foreach (Transform child in this.transform)
        {
            if(child.gameObject.activeSelf)
            {
                activeVirusExists = true;
                break;
            }
        }
        return !activeVirusExists;
    }
}

