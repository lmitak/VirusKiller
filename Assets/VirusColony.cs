using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirusColony : MonoBehaviour {

    public GameManager manager;
    public float dropChancePercentage;
    public GameObject[] items;

    private List<int> unactiveVirusesIndex;
    private bool activeVirusExists;
    private bool gameWonCalled;

	// Use this for initialization
	void Start () {
        unactiveVirusesIndex = new List<int>();
        activeVirusExists = true;
        gameWonCalled = false;
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
                    if (Random.Range(0f, 1f) < (dropChancePercentage / 100))    /**try dropping ability by chance**/
                    {
                        Instantiate(items[0], transform.GetChild(i).position, Quaternion.identity);
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
}
