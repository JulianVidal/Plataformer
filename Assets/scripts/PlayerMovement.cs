/// <summary>
/// This is the movement of the character that I have implemented with some improvements.
/// I had to learn how to the new IDE Rider that I had never used. I have also
/// create a tile map to draw onto the screen and make a level for the player.
/// I had never used a tilemap before, it is the concept of the screen
/// being a grid and drawing onto it with blocks. It took sometime to get
/// basic blocks on the screen and my quickly drawned images don't look very good
/// but it works, until I get something some art from Javid. I had to learn how
/// to do pixelart but after a couple of tries, I got something reasonable.
/// </summary>

using System;
using UnityEngine;

namespace scripts
{
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody2D _rb;
        private Vector3 _position;


        //Forces that changes the movement of the player
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


    }
}
