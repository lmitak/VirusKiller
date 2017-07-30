using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Inventory : MonoBehaviour {

    public GameObject inventoryButton;
    public GameObject list;
    public GameObject listItem;
    public int poolSize = 6;

    private GameObject displayedItem;   // GameObject that displays sprite in the button
    private int indexOfCurrentItemInList;
    public List<Item> items = new List<Item>();
    private List<GameObject> listObjects;
    private GameObject contentList;

    private UnityAction longClickListener;
    private bool isLongClickTriggered = false;

    void Awake()
    {
        longClickListener = new UnityAction(OnInventoryButtonLongClick);
    }

	// Use this for initialization
	void Start () {
        listObjects = new List<GameObject>();

        displayedItem = this.transform.GetChild(0).GetChild(0).gameObject;
        displayedItem.SetActive(false);
        indexOfCurrentItemInList = -1;

        Button invBtn = inventoryButton.GetComponent<Button>();
        invBtn.onClick.AddListener(OnInventoryButtonClick);

        this.contentList = this.list.transform.GetChild(0).GetChild(0).gameObject;
        this.PoolListObjects(this.poolSize);
	}

    void OnEnable()
    {
        EventManager.StartListening(EventManager.EventType.LONG_CLICK, longClickListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.EventType.LONG_CLICK, longClickListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(Item item)
    {
        bool isConditionMet = false;
        //traverse through all items and check if it exists
        for (int i = 0; i < items.Count; i++)
        {
            //if it exists, increment the amount
            if (items[i].GetType() == item.GetType())
            {
                items[i].amount++;
                isConditionMet = true;
                indexOfCurrentItemInList = i;
                break;
            }
        }
        //if item has not been found
        if (!isConditionMet)
        {
            //clone the item
            Item newItem = Instantiate<Item>(item, contentList.transform.position, Quaternion.identity);
            newItem.transform.parent = contentList.transform;
            newItem.copyItemProperties(item);
            Destroy(newItem.GetComponent<Rigidbody2D>());
            
            if(item.GetType().IsSubclassOf(typeof (ItemBall)))
            {
                ((ItemBall)newItem).ball = ((ItemBall)item).ball;
            } else if (item.GetType().IsSubclassOf(typeof(ItemPaddle)))
            {
                ((ItemPaddle)newItem).paddle = ((ItemPaddle)item).paddle;
            }
            newItem.amount = 1;
            //add clone to the list
            items.Add(newItem);
            indexOfCurrentItemInList = items.Count - 1;
        }

        displayedItem.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        if(!displayedItem.activeSelf)
        {
            displayedItem.SetActive(true);
        }
    }

    public void OnInventoryButtonClick()
    {       
        if(isLongClickTriggered)
        {
            isLongClickTriggered = false;
            return;
        }
        Debug.Log("short click");

        if (indexOfCurrentItemInList != -1)
        {
            // apply effect of current item
            items[indexOfCurrentItemInList].ApplyEffect();
            
            // decrement current item amount and check if it is lower than 1
            if (--items[indexOfCurrentItemInList].amount < 1)
            {
                // if it is, remove the item from the list
                items.RemoveAt(indexOfCurrentItemInList);
                // check if there are items in list, and if there are, select last item in list
                if (items.Count > 0)
                {
                    indexOfCurrentItemInList = items.Count - 1;
                    displayedItem.GetComponent<Image>().sprite = items[indexOfCurrentItemInList].sprite;
                }
                // if there are no items in list, set all to null and disable displayedItem
                else
                {
                    indexOfCurrentItemInList = -1;
                    displayedItem.SetActive(false);
                }
            }
        }
    }

    private void OnInventoryButtonLongClick()
    {
        Debug.Log("Long click pressed");
        isLongClickTriggered = true;
        if(items.Count > 1)
        {
            this.FillListWithItems();
            this.list.SetActive(true);
            this.inventoryButton.SetActive(false);
        }      
    }

    // Add items from item list to the listObjects list and display those listObjects
    private void FillListWithItems()
    {
        // while there is less listObjects than items, instantiate more listObjects
        this.PoolListObjects(items.Count);
        // traverse through listObjects and add items to it
        int i = 0;
        foreach (GameObject row in listObjects)
        {
            if (items.Count > i)
            {
                ListItem listItem = listObjects[i].GetComponent<ListItem>();
                listItem.item = items[i];
                listItem.SetUp();
                listObjects[i].SetActive(true);
            }
            else
            {
                listObjects[i].SetActive(false);
            }
            i++;
        }
    }

    private void PoolListObjects(int count)
    {
        while(this.listObjects.Count < count)
        {
            listObjects.Add(this.InstantiateListItem());
        }
    }

    public void OnListItemClick(GameObject row)
    {
        this.indexOfCurrentItemInList = this.listObjects.IndexOf(row);
        this.list.SetActive(false);
        displayedItem.GetComponent<Image>().sprite = row.GetComponent<ListItem>().item.sprite;
        this.inventoryButton.SetActive(true);
    }

    private GameObject InstantiateListItem()
    {
        GameObject row = Instantiate<GameObject>(listItem, contentList.transform);
        ListItem rowListItem = row.GetComponent<ListItem>();
        rowListItem.Init();
        row.SetActive(false);
        Button btn = row.GetComponent<Button>();
        btn.onClick.AddListener(() => OnListItemClick(row));
        return row;
    }
}
