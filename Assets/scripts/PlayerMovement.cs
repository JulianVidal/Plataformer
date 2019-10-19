/// <summary>
/// After talking to Javid of what features I should add, he said that I should do crouching.
/// We brainstormed some solutions and landed on switching the hitboxes. At first I didn't know
/// how I could do that, due to my limited knowledge of Unity but Javid pointed to some websites
/// were I could figure out what to do.
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
            Crouch();

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("ground"))
            {
                _canJump = true;
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
          else
          {
              gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
              gameObject.GetComponent<CircleCollider2D>().enabled = false;

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
