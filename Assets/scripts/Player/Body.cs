using UnityEngine;
public class Body
{
    public readonly Move Move;
    public Body(GameObject gameObject, Rigidbody2D rigidBody2D, float walkForce, float jumpForce, float floatForce)
    {
        Move = new Move(gameObject, rigidBody2D, walkForce, jumpForce, floatForce);
    }

    public float GetChargeP()
    {
        return Move.GetChargeP();
    }
    
    public void SetChargeP(float charge)
    {
        Move.SetChargeP(charge);
    }

    public float GetHealthP()
    {
        return Move.GetHealthP();
    }

    public void SetHealthP(float health)
    {
        Move.SetHealthP(health);
    }

}