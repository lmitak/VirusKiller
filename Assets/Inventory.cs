using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private Item currentItem;
    private GameObject displayedItem;
    private int indexOfCurrentItemInList;
    private List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        displayedItem = this.transform.GetChild(0).GetChild(0).gameObject;
        displayedItem.SetActive(false);
        currentItem = null;
        indexOfCurrentItemInList = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(Item item)
    {
        this.currentItem = item;
        items.Add(item);
        indexOfCurrentItemInList = items.Count - 1;
        displayedItem.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        if(!displayedItem.activeSelf)
        {
            displayedItem.SetActive(true);
        }
    }

    public void OnInventoryButtonClick()
    {
        if(currentItem != null)
        {
            currentItem.ApplyEffect();
            items.RemoveAt(indexOfCurrentItemInList);
            if(items.Count > 0)
            {
                indexOfCurrentItemInList = items.Count - 1;
                currentItem = items[indexOfCurrentItemInList];
            }
            else
            {
                currentItem = null;
                indexOfCurrentItemInList = -1;
                displayedItem.SetActive(false);
            }
        }
    }
}
