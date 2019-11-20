using System;
using UnityEngine;
public class Movement
{
    private Rigidbody2D _rb;
    private float _jumpForce;
    private float _walkForce;
    private float _floatForce;

    private const int MaxJumps = 2;
    private const int MaxTimeJump = 10;
    private int jumps = MaxJumps;
    private int jumpTime = MaxTimeJump;

    private float standUp = false;

    public Collision collision;



    public Movement(Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        _rb = rigidbody;
        _jumpForce = jumpForce;
        _walkForce = walkForce;
        _floatForce = floatForce;
        collision = new Collision(rigidbody.transform.position);
    }

    public void moveRight()
    {
        if (Input.GetKey("right"))
        {
            _rb.AddForce(new Vector2(_walkForce * Time.fixedDeltaTime, 0));
        }
    }
    public void moveLeft()
    {
        if (Input.GetKey("left"))
        {
            _rb.AddForce(new Vector2(-_walkForce * Time.fixedDeltaTime, 0));
        }
    }

    public Rigidbody2D getBody()
    {
        return _rb;
    }

    public void jump()
    {
        collision.update(_rb.transform.position);

        if (collision.checkGround())
        {
            jumps = MaxJumps;
            jumpTime = MaxTimeJump;
        }

        if (Input.GetKeyDown("up"))
        {
            Debug.Log("Key");
            Debug.Log(jumps);
            Debug.Log(jumpTime);
            Debug.Log(collision.checkGround());

        }

        if (Input.GetKeyDown("up") && jumps > 0)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
            _rb.AddForce(new Vector2(0, _jumpForce));
            jumps--;

        }

        if (Input.GetKey("up") && jumpTime > 0 && jumps < MaxJumps)
        {
            _rb.AddForce(new Vector2(0, _floatForce));
            jumpTime--;
        }

    }

    public void crouch()
    {
        collision.update(_rb.transform.position);

        if (!collision.checkRoof())
        {
            standUp = true;
        }
        else
        {
            standUp = false;
        }

        if (Input.GetKeyDown("down") && standUp == true) {

        }


    }

}