using System;
using UnityEngine;

public class Player
{
    private readonly Body _body;

    public Player(GameObject gameObject, Rigidbody2D rigidBody2D, float walkForce, float jumpForce, float floatForce)
    {
        _body = new Body(gameObject, rigidBody2D, walkForce, jumpForce, floatForce);
    }

    public void Update()
    {
        _body.Move.Right();
        _body.Move.Left();

        _body.Move.Jump();

        _body.Move.Crouch();

        _body.Attack.Melee();
    }

    public float GetChargeP()
    {
        return _body.GetChargeP();
    }

    public float GetHealthP()
    {
        return _body.GetHealthP();
    }


}