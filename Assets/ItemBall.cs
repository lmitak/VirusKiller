using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBall : Item {

    public Ball ball { get; set; }

    void Start()
    {
        this.itemType = ItemType.BallRelated;
    }

}
