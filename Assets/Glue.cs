using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : ItemPaddle {

    public override void ApplyEffect()
    {
        this.paddle.ApplyStickyPaddle();
    }

    
}
