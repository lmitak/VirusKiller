using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : ItemBall {

    public float duration = 15.0f;

    private float ballCurrentSpeed;

    public override void ApplyEffect()
    {
        ballCurrentSpeed = this.ball.speed;
        this.ball.speed = ballCurrentSpeed / 2;
        Invoke("ReturnSpeedToNormal", duration);
    }


    private void ReturnSpeedToNormal()
    {
        this.ball.speed = this.ballCurrentSpeed;
    }

}
