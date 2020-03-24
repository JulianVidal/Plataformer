using System;
using UnityEngine;
public class Move
{
    private Rigidbody2D _rb;
    private GameObject _gb;
    private float _jumpForce;
    private float _walkForce;
    private float _floatForce;

    private const int MaxJumps = 2;
    private const int MaxTimeJump = 10;
    private int jumps = MaxJumps;
    private int jumpTime = MaxTimeJump;

    private bool standUp = false;

    public Collision _collision;

    public Attack _attack;

    private float MaxCharge = 1000;

    private float charge = 0;

    private float MaxHealth = 100;
    private float health;



    public Move(GameObject gameObject, Rigidbody2D rigidbody, float walkForce, float jumpForce, float floatForce)
    {
        _rb = rigidbody;
        _gb = gameObject;
        _jumpForce = jumpForce;
        _walkForce = walkForce;
        _floatForce = floatForce;
        _collision = new Collision(rigidbody.transform.position);
        _attack = new Attack(gameObject);
        health = MaxHealth;
    }

    public void right()
    {
        if (Input.GetKey("right"))
        {
            _rb.AddForce(new Vector2(_walkForce * Time.fixedDeltaTime, 0));
            addCharge();
        }
    }
    public void left()
    {
        if (Input.GetKey("left"))
        {
            _rb.AddForce(new Vector2(-_walkForce * Time.fixedDeltaTime, 0));
            addCharge();
        }
    }

    public Rigidbody2D getBody()
    {
        return _rb;
    }

    public void jump()
    {
        _collision.update(_rb.transform.position);

        if (_collision.checkGround())
        {
            jumps = MaxJumps;
            jumpTime = MaxTimeJump;
        }

        if (Input.GetKeyDown("up") && jumps > 0 && standUp)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
            _rb.AddForce(new Vector2(0, _jumpForce));
            jumps--;

        }

        if (Input.GetKey("up") && jumpTime > 0 && jumps == MaxJumps - 1)
        {
            _rb.AddForce(new Vector2(0, _floatForce));
            jumpTime--;
        }
    }

    public void crouch()
    {
        _collision.update(_rb.transform.position);

        if (Input.GetKey("down"))
        {
            _gb.GetComponent<BoxCollider2D>().enabled = false;
            _gb.GetComponent<CircleCollider2D>().enabled = true;

        }

        standUp = !_collision.checkRoof();

        if (standUp == true && !Input.GetKey("down"))
        {
            _gb.GetComponent<BoxCollider2D>().enabled = true;
            _gb.GetComponent<CircleCollider2D>().enabled = false;
        }


    }

    private void addCharge()
    {

        Vector2 stop = new Vector2(0, _rb.velocity.y);

        if (!(_rb.velocity.Equals(stop)) && charge <= MaxCharge)
        {
            charge++;
            _walkForce++;
        }
    }

    public float getChargeP()
    {
        return charge / MaxCharge;
    }

    public float getHealthP()
    {
        return health / MaxHealth;
    }
}
