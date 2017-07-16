using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VirusColony : MonoBehaviour, DeathAnnouncement {

    public GameManager manager;
    public float totalDropChance;
    public GameObject[] items;
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
	
	
	void Update () {
        activeVirusExists = false;
        for (int i = 0; i < transform.childCount && transform.childCount > unactiveVirusesIndex.Count; i++)
        {
            /**check if active viruses exists**/
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                /**if virus is not active, check if it is in virus list, if not add it to the list.
                 * If virus is already in the list, virus was unactive before this update frame**/
                if (unactiveVirusesIndex.IndexOf(i) == -1)
                {
                    unactiveVirusesIndex.Add(i);
                    /**try dropping ability by chance**/
                    float dice = UnityEngine.Random.Range(0f, 100f);
                    if (dice < totalDropChance)    
                    {
                        // if item should be dropped randomize which item will be dropped
                        dice = UnityEngine.Random.Range(0f, 100f);

                        int itemIndex = 0;
                        foreach(RangeInt range in itemsDropRange)
                        {
                            if(dice >= range.start && dice <= range.end)
                            {
                                Instantiate(drops[itemIndex].item, transform.GetChild(i).position, Quaternion.identity);
                                break;
                            }
                            itemIndex++;
                        }
                    }
                }
            }
            else
            {
                activeVirusExists = true;
            }

        }

        /**if there is no active viruses game has been won**/
        if (!activeVirusExists && !gameWonCalled)
        {
            gameWonCalled = true;
            manager.GameWon();
        }
    }

    public void ImGonnaDie(Enemy enemy)
    {
        enemyValueDisplay.ShowEnemyValue(enemy.points, enemy.transform.position);
        scoreSystem.IncreaseScore(enemy.points);
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

