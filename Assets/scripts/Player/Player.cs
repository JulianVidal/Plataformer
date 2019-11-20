using System;
using UnityEngine;

public class Player
{
    public Movement body;

    public Player(Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        body = new Movement(rigidbody, walkForce, jumpForce, floatForce);
    }

    public void update()
    {
        body.moveRight();
        body.moveLeft();

        body.jump();
    }

}