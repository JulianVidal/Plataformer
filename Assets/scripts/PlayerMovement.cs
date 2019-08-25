using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngineInternal;

public class PlayerMovement : MonoBehaviour
{
    private bool _canJump = false; // Jump when touching the ground.
    private bool _standUp = true; // Stand up when there is no object on top of the player

    /// <summary>
    /// These variables are used to check if the player
    /// is touching the ground or if the player has a roof
    /// of top of the head.
    /// 
    /// Corners will shoot rays up or down to find collisions
    /// </summary>
    //Values of the different corners of the player hit box
    private Vector2 _bottomLeft;
    private Vector2 _bottomRight;
    
    private Vector2 _topLeft;
    private Vector2 _topRight;
    
    //Direction of the ray (1 up, -1 down)
    private Vector2 _bottomDir = new Vector2(0, -1);
    private Vector2 _topDir = new Vector2(0, 1);
    
    private float _rayOriginOffset = 0.1f; // Offset stops the ray from colliding with the player
    private float _rayDistance = 0.0001f;  // How far the ray travels

    
    //Forces that changes the movement of the player
    public float jumpForce = 200f;
    public float walkForce = 1000f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Checks if the player is pressing left or right key
        // Applies a walkForce to the respective direction
        if (Input.GetKey("right"))
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(walkForce * Time.deltaTime, 0f));

        } else if (Input.GetKey("left"))
        {            

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-walkForce * Time.deltaTime, 0f));

            
        }
        
        //Calculates the corners of the hit box
        _bottomLeft  = new Vector2(gameObject.transform.position.x - 0.5f + _rayOriginOffset, gameObject.transform.position.y - 1 - _rayOriginOffset);
        _bottomRight = new Vector2(gameObject.transform.position.x + 0.5f - _rayOriginOffset, gameObject.transform.position.y - 1 - _rayOriginOffset);

        _topLeft  = new Vector2(gameObject.transform.position.x - 0.5f + _rayOriginOffset, gameObject.transform.position.y + _rayOriginOffset);
        _topRight = new Vector2(gameObject.transform.position.x + 0.5f - _rayOriginOffset, gameObject.transform.position.y + _rayOriginOffset);
        
        // Casts ray and returns collisions
        RaycastHit2D hitBottomLeft  = CastRay(_bottomLeft, _bottomDir);
        RaycastHit2D hitBottomRight = CastRay(_bottomRight, _bottomDir);

        RaycastHit2D hitTopLeft  = CastRay(_topLeft, _topDir);
        RaycastHit2D hitTopRight = CastRay(_topRight, _topDir);
        
        
        // Lets the player jump if there is a collision with the ground
        if (hitBottomLeft.collider != null || hitBottomRight.collider != null )
        {
            _canJump = true;
        }
        
        // Lets the player stand up if there are no collision on top
        if (hitTopLeft.collider != null || hitTopRight.collider != null )
        {

            _standUp = false;
        }
        else
        {
            _standUp = true;
        }

            
        // Checks if the player presses the up arrow
        //  and if the player can jump
        //  jumps with jumpForce of force upward
        if (Input.GetKeyDown("up") && _canJump)
        {
            _canJump = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        
        // Checks if the player is pressing the down key
        // It will disable the long hit box and enable the smaller one
        // Will be reverted if the player stop pressing down key and
        // there are no collision of top
        if (Input.GetKey("down"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;

        }
        else if (_standUp)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

        }
        

    }

    // Shoots a ray from an origins, towards a direction with the
    // distance of _rayDistance and returns collisions 
    // otherwise null
    private RaycastHit2D CastRay(Vector2 origin, Vector2 direction)
    {

        Debug.DrawRay(origin, direction, Color.red, 0f, false);
       
        return Physics2D.Raycast(origin, direction, _rayDistance);
        
    }
    
}
 