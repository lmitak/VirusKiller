using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPaddle : Item {

    public Paddle paddle { get; set; }

    void Start()
    {
        this.itemType = ItemType.PaddleRelated;
    }

}
