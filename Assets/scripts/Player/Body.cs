using UnityEngine;
public class Body
{
    public readonly Move Move;
    public readonly Attack Attack;
    public Body(GameObject gameObject, Rigidbody2D rigidBody2D, float walkForce, float jumpForce, float floatForce)
    {
        Move = new Move(gameObject, rigidBody2D, walkForce, jumpForce, floatForce);
        Attack = new Attack(gameObject);
    }

    public float GetChargeP()
    {
        return Move.GetChargeP();
    }

    public float GetHealthP()
    {
        return Move.GetHealthP();
    }


}