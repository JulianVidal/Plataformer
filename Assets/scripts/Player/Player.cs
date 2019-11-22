using System;
using UnityEngine;

public class Player
{
    public Body body;

    public Player(GameObject gameObject, Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        body = new Body(gameObject, rigidbody, walkForce, jumpForce, floatForce);
    }

    public void update()
    {
        body._move.right();
        body._move.left();

        body._move.addCharge();

        body._move.jump();

        body._move.crouch();

        body._attack.melee();
    }

    public float getChargeP()
    {
        return body.getChargeP();
    }

}