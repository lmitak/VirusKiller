using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public Sprite sprite;
    protected ItemType itemType;

    public abstract void ApplyEffect();

    public ItemType GetItemType()
    {
        return this.itemType;
    }

    void Start()
    {
        this.itemType = ItemType.General;
    }

}

public enum ItemType
{
    General,
    PaddleRelated,
    BallRelated
}
