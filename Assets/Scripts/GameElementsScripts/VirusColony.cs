using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VirusColony : MonoBehaviour, DeathAnnouncement {

    public GameManager manager;
    public float totalDropChance;
    public ItemDrop[] drops;
    public DisplayEnemyValue enemyValueDisplay;
    public ScoreSystem scoreSystem;

    private List<int> unactiveVirusesIndex;
    private bool activeVirusExists;
    private bool gameWonCalled;
    private List<RangeInt> itemsDropRange;

	// Use this for initialization
	void Start () {
        unactiveVirusesIndex = new List<int>();
        activeVirusExists = true;
        gameWonCalled = false;


        itemsDropRange = new List<RangeInt>();
        this.CheckItemDropChance();
        this.InitItemsDropRange();
	}
	
	void Update () { }

    /// <summary>
    /// Called when enemy should die
    /// </summary>
    /// <param name="enemy">GameObject that should die</param>
    public void ImGonnaDie(Enemy enemy)
    {
        // calculate if item sholud be dropped
        if(this.ShouldDropItem())
        {
            this.DropItem(enemy.gameObject);
        }
        enemyValueDisplay.ShowEnemyValue(enemy.points, enemy.transform.position);
        scoreSystem.IncreaseScore(enemy.points);
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

    /// <summary>
    /// Checks if item sholud be dropped by comparing totalDropChange with a random number in range of 0 to 100
    /// Ex: If totalDropChance is 40, than random number must be below 40 to drop an item
    /// </summary>
    /// <returns>True if randomized number is less than a totalDropChance</returns>
    private bool ShouldDropItem()
    {
        return UnityEngine.Random.Range(0f, 100f) < totalDropChance;
    }

    /// <summary>
    /// Calculate which item sholud be dropped and instantiate GameObject of that type
    /// </summary>
    private void DropItem(GameObject enemy)
    {
        float dice = UnityEngine.Random.Range(0f, 100f);
        int i = 0;
        foreach (RangeInt range in itemsDropRange)
        {
            if (dice >= range.start && dice <= range.end)
            {
                Instantiate(drops[i].item, enemy.transform.position, Quaternion.identity);
                break;
            }
            i++;
        }
    }

    /// <summary>
    /// Check if sum of all items drop chance is equal to 100
    /// </summary>
    /// <returns></returns>
    private bool CheckItemDropChance()
    {
        int sum = 0;
        foreach(ItemDrop drop in drops)
        {
            sum += drop.dropChance;
        }

        if(sum != 100)
        {
            // If sum is not 100 display warning
            Debug.LogWarning(this.name + ": Sum of items drop chance should be 100, instead it is " + sum);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Initialize itemsDropRange list
    /// </summary>
    private void InitItemsDropRange()
    {
        itemsDropRange.Add(new RangeInt(0, drops[0].dropChance));
        for (int i = 1; i < drops.Length; i++)
        {
            RangeInt range = new RangeInt(itemsDropRange[i - 1].end, drops[i].dropChance);
            itemsDropRange.Add(range);
        }
    }
}

[System.Serializable]
public struct ItemDrop
{
    public GameObject item;
    [Tooltip("Percentage of Total Drop Chance(Sum of items should be 100)")]
    public int dropChance;
}

