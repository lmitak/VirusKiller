using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public Sprite sprite;
    protected ItemType itemType;
    public int amount { get; set; }

    public abstract void ApplyEffect();

    public ItemType GetItemType()
    {
        return this.itemType;
    }

    void Start()
    {
        this.itemType = ItemType.General;
    }

    public void copyItemProperties(Item otherItem)
    {
        this.sprite = otherItem.sprite;
        this.itemType = otherItem.itemType;
        this.amount = otherItem.amount;
    }

}

public enum ItemType
{
    General,
    PaddleRelated,
    BallRelated
}
