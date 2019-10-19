/// <summary>
/// After taking some time to think I figured out a way to know if the player is on the
/// so they can jump or if they are under something and can't uncrouch. I came up with rays,
/// rays that would be casted from the corner of the player to check if the player is touhcing
/// the floor or the ceiling. After reading the documentation of Raycasting I was able to have
/// a result I am more satisfied with.
/// </summary>

using System;
using UnityEngine;

namespace scripts
{
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody2D _rb;
        private Vector3 _position;

        private bool _canJump = true; // Jump when touching the ground.
        private bool _standUp = true; // Stand up when there is no object on top of the player

        private bool _isDoor;

        /// <summary>
        /// These variables are used to check if the player
        /// is touching the ground or if the player has a roof
        /// of top of the head.
        ///
        /// Corners will shoot rays up or down to find collisions
        /// </summary>
        //Values of the different corners of the player hit box

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

          if (Input.GetKey("up") && _canJump)
          {
              _rb.AddForce(new Vector2(0, 50));
          }

        }

        private void Move() {
            // Checks if the player is pressing left or right key
          // Applies a walkForce to the respective direction
          if (Input.GetKey("right"))
          {
              _rb.AddForce(new Vector2(walkForce * Time.deltaTime, 0));

          }
          else if (Input.GetKey("left"))
          {
              _rb.AddForce(new Vector2(-walkForce * Time.deltaTime, 0));
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
      _canJump = true;


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

}

    }
}
