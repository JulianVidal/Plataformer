using System;
using UnityEngine;
public class Body
{
    public Move _move;
    public Attack _attack;
    public Body(GameObject gameObject, Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        _move = new Move(gameObject, rigidbody, walkForce, jumpForce, floatForce);
        _attack = new Attack(gameObject);
    }

    public float getChargeP()
    {
        return _move.getChargeP();
    }
}