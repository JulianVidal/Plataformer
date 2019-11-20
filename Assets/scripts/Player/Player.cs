using System;
using UnityEngine;

public class Player
{
    public Movement body;

    public Player(GameObject gameObject, Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        body = new Movement(gameObject, rigidbody, walkForce, jumpForce, floatForce);
    }

    public void update()
    {
        body.moveRight();
        body.moveLeft();

        body.jump();
    }

}