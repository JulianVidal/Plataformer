/// <summary>
/// Next I added how to jump, this became quite tricky. I still haven't come up
/// with a solution but I got something that works mostly. Jumping is an upward force
/// similar to the left and right forces from moving except the player should only jump if
/// the player is touching the ground, otherwise it will jump infinelty. Trying to figure
/// out a way to know if the player is touching the ground, when there are different height the player
/// can be in became quite difficult. For now I settled on, if the player is touching something, it can jump.
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

            if (_canJump && Input.GetKey("space")) {
              _rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime));

            }

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                _canJump = true;
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


    }
}
