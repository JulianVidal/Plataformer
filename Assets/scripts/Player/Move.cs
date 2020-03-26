using UnityEngine;
public class Move
{
    private readonly Rigidbody2D _rb;
    private readonly GameObject _gb;
    private readonly float _jumpForce;
    private float _walkForce;
    private readonly float _floatForce;

    private const int MaxJumps = 2;
    private const int MaxTimeJump = 10;
    private int _jumps = MaxJumps;
    //private int _jumpTime = MaxTimeJump;

    private bool _standUp;

    private readonly Collision _collision;

    //private Attack _attack;

    private const float MaxCharge = 1000;

    private float _charge;

    private const float MaxHealth = 100;
    private float _health;



    public Move(GameObject gameObject, Rigidbody2D rigidBody2D, float walkForce, float jumpForce, float floatForce)
    {
        _rb = rigidBody2D;
        _gb = gameObject;
        _jumpForce = jumpForce;
        _walkForce = walkForce;
        _floatForce = floatForce;
        _collision = new Collision(rigidBody2D.transform.position);
        //_attack = new Attack(gameObject);
        _health = MaxHealth;
    }

    public void Right()
    {
        if (Input.GetKey("right"))
        {
            _gb.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
            _rb.AddForce(new Vector2(_walkForce * Time.fixedDeltaTime, 0));
            AddCharge();
        }
    }
    
    public void Left()
    {
        if (Input.GetKey("left"))
        {
            _gb.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 180f, 0f);
            _rb.AddForce(new Vector2(-_walkForce * Time.fixedDeltaTime, 0));
            AddCharge();
        }
    }

    public void Jump()
    {
        _collision.Update(_rb.transform.position);

        if (_collision.CheckGround())
        {
            _jumps = MaxJumps;
            //_jumpTime = MaxTimeJump;
        }

        if (Input.GetKeyDown("up") && _jumps > 0 && _standUp)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
            _rb.AddForce(new Vector2(0, _jumpForce));
            _jumps--;
            //_jumpTime = MaxTimeJump;
        }

        /*if (Input.GetKey("up") && _jumpTime > 0 && _jumps == MaxJumps - 1)
        {
            _rb.AddForce(new Vector2(0, _floatForce));
            _jumpTime--;
        }*/
    }

    public void Crouch()
    {
        _collision.Update(_rb.transform.position);

        if (Input.GetKey("down"))
        {
            _gb.GetComponent<BoxCollider2D>().enabled = false;
            _gb.GetComponent<CircleCollider2D>().enabled = true;

        }

        _standUp = !_collision.CheckRoof();

        if (_standUp == true && !Input.GetKey("down"))
        {
            _gb.GetComponent<BoxCollider2D>().enabled = true;
            _gb.GetComponent<CircleCollider2D>().enabled = false; 
        }
        
    }

    private void AddCharge()
    {

        var stop = new Vector2(0, _rb.velocity.y);

        if (!(_rb.velocity.Equals(stop)) && _charge <= MaxCharge)
        {
            _charge++;
            _walkForce++;
        }

    }

    public float GetChargeP()
    {
        return _charge / MaxCharge;
    }
    
    public void SetChargeP(float charge)
    {
        _charge = charge * MaxCharge;
    }

    public float GetHealthP()
    {
        return _health / MaxHealth;
    }
    
    public void SetHealthP(float health)
    {
        _health = health * MaxHealth;
    }
}
