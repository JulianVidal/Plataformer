<<<<<<< HEAD
﻿using System;
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

    private bool _isDoor = false;
    
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
        if (Input.GetKeyDown("up") && _canJump && !_isDoor)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            _canJump = false;

        }
        else if (Input.GetKeyDown("up") && _isDoor)
        {
            gameObject.transform.position = new Vector3(0.5f, -1, 0);
            _isDoor = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            _isDoor = true;
        }
    }
}
 
=======
/// <summary>
/// I talked again to Javid about what other features we should add,
/// he asked me to add double jumping, dash and charge jump. After some thinking
/// and ingenuity, the code I already had could be bended have these features.
/// I would let the player jump despite not touhcing the ground, the longer the player
/// presses the spacebar the more force I would add and when the player pressed a button while jumping,
/// a force would push it to its direction.
/// </summary>

using System;
using UnityEngine;

namespace scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private int _playerDir;
        private int _wallDir;

        private const int MaxJumps = 2;
        private const int MaxDash = 1;
        private const int MaxTimeJump = 10;

        private Rigidbody2D _rb;
        private Vector3 _position;

        private int _canJump = MaxJumps; // Jump when touching the ground.
        private bool _standUp = true; // Stand up when there is no object on top of the player
        private int _canDash = MaxDash;
        private bool _canWallJump;

        private bool _isDoor;

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

        private Vector2 _midLeft;
        private Vector2 _midRight;

        //Direction of the ray (1 up, -1 down)
        private readonly Vector2 _bottomDir = new Vector2(0, -1);
        private readonly Vector2 _topDir = new Vector2(0, 1);

        //Direction of the ray (1 right, -1 left)
        private readonly Vector2 _rightDir = new Vector2(1, 0);
        private readonly Vector2 _leftDir = new Vector2(-1, 0);

        private float _rayOriginOffset = 0.1f; // Offset stops the ray from colliding with the player
        private float _rayDistance = 0.0001f;  // How far the ray travels

        //Forces that changes the movement of the player
        public float jumpForce = 200f;
        public float walkForce = 1000f;
        public float dashForce = 500f;
        public int   jumpTime = 5;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _position = _rb.transform.position;

        }

        // Update is called once per frame
        private void Update() {

            //Basic movement of the player
            Move();

            _position = _rb.transform.position;

            CheckSides();

            Jump();

            Crouch();

            if (Input.GetKey(".") && _canJump < MaxJumps && _canDash > 0) {

                if (_playerDir == 1)
                {
                    _rb.AddForce(new Vector2(dashForce, 0));

                    _canDash--;
                }
                else if (_playerDir == 0)
                {
                    _rb.AddForce(new Vector2(-dashForce, 0));
                    _canDash--;
                }


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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                _isDoor = true;
            }
        }

        private void Crouch() {
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

        private void Jump() {
          // Checks if the player presses the up arrow
          //  and if the player can jump
          //  jumps with jumpForce of force upward
          if (Input.GetKeyDown("up") && _canJump > 0 && !_isDoor)
          {
              _rb.AddForce(new Vector2(0, jumpForce));
              _canJump--;

          }
          else if (Input.GetKeyDown("up") && _isDoor)
          {
              gameObject.transform.position = new Vector3(0.5f, -1, 0);
              _isDoor = false;
          }
          else if (Input.GetKeyDown("up") && _canWallJump && _canJump != MaxJumps)
          {
              _rb.AddForce(new Vector2(0, jumpForce));

              if (_wallDir == 1)
              {
                 _rb.AddForce(new Vector2(_wallDir * 100, 0));
              }
              else
              {
                  _rb.AddForce(new Vector2(_wallDir * 100, 0));
              }
          }

          if (Input.GetKey("up") && jumpTime > 0 && !_isDoor && _canJump < MaxJumps)
          {
              _rb.AddForce(new Vector2(0, 50));
              jumpTime--;
          }

        }

        private void Move() {
            // Checks if the player is pressing left or right key
          // Applies a walkForce to the respective direction
          if (Input.GetKey("right"))
          {
              _rb.AddForce(new Vector2(walkForce * Time.deltaTime, 0));
              _playerDir = 1;

          }
          else if (Input.GetKey("left"))
          {
              _rb.AddForce(new Vector2(-walkForce * Time.deltaTime, 0));
              _playerDir = 0;
          }

        }

        private void CheckSides() {
          //Calculates the corners of the hit box
          _bottomLeft  = new Vector2(_position.x - 0.5f + _rayOriginOffset, _position.y - 1 - _rayOriginOffset);
          _bottomRight = new Vector2(_position.x + 0.5f - _rayOriginOffset, _position.y - 1 - _rayOriginOffset);

          _topLeft  = new Vector2(_position.x - 0.5f + _rayOriginOffset, _position.y + 1 + _rayOriginOffset);
          _topRight = new Vector2(_position.x + 0.5f - _rayOriginOffset, _position.y + 1 + _rayOriginOffset);

          _midLeft  = new Vector2(_position.x - 0.5f + _rayOriginOffset, _position.y + _rayOriginOffset);
          _midRight = new Vector2(_position.x + 0.5f - _rayOriginOffset, _position.y + _rayOriginOffset);

          // Casts ray and returns collisions
          RaycastHit2D hitTopLeftN  = CastRay(_midLeft, _topDir);
          RaycastHit2D hitTopRightN = CastRay(_midRight, _topDir);

          RaycastHit2D hitBottomLeftS  = CastRay(_bottomLeft, _bottomDir);
          RaycastHit2D hitBottomRightS = CastRay(_bottomRight, _bottomDir);

          // Casts ray and returns collisions
          RaycastHit2D hitBottomLeftE  = CastRay(_bottomLeft, _leftDir);
          RaycastHit2D hitTopLeftE  = CastRay(_topLeft, _leftDir);

          RaycastHit2D hitBottomRightW = CastRay(_bottomRight, _rightDir);
          RaycastHit2D hitTopRightW = CastRay(_topRight, _rightDir);


          // Lets the player jump if there is a collision with the ground
          if (hitBottomLeftS.collider || hitBottomRightS.collider)
          {
              _canJump = MaxJumps;
              _canDash = MaxDash;
              jumpTime = MaxTimeJump;

          }

          // Lets the player stand up if there are no collision on top
          if (hitTopLeftN.collider || hitTopRightN.collider)
          {

              _standUp = false;
          }
          else
          {
              _standUp = true;
          }

          // Lets the player stand up if there are no collision on top
          if (hitBottomLeftE.collider || hitBottomRightW.collider || hitTopLeftE.collider || hitTopRightW.collider)
          {
              _canWallJump = true;

              if (hitTopRightW || hitBottomRightW)
              {
                 _wallDir = 1;
              }
              else if (hitTopLeftE || hitBottomLeftE)
              {
                  _wallDir = -1;
              }
          }
          else
          {
              _canWallJump = false;
          }

        }

    }
}
>>>>>>> c0eac987eb2ea5b5a7937ed53df79be2fff5c1f2
