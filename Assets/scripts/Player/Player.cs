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
    }

    public float GetChargeP()
    {
        return _body.GetChargeP();
    }
    
    public void SetChargeP(float charge)
    {
        _body.SetChargeP(charge);
    }


    public float GetHealthP()
    {
        return _body.GetHealthP();
    }

    public void SetHealthP(float health)
    {
      _body.SetHealthP(health);
    }


}