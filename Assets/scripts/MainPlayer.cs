using System;
using UnityEngine;

namespace scripts
{
    public class MainPlayer : MonoBehaviour
    {
        private Player player;
        private Bar chargeBar;

        private Bar healthBar;

        //Forces that changes the movement of the player
        public float jumpForce = 200f;
        public float walkForce = 1000f;
        public float floatForce = 50f;
        /*public float dashForce = 500f;*/
        /*public int   jumpTime = 5;*/

        // Start is called before the first frame update
        private void Start()
        {

            chargeBar = new Bar(transform.Find("/Main Camera").Find("ChargeBar").Find("Bar"));

            healthBar = new Bar(transform.Find("/Main Camera").Find("HealthBar").Find("Bar"));

            player = new Player(gameObject, GetComponent<Rigidbody2D>(), walkForce, jumpForce, floatForce);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            player.update();
            chargeBar.setSize(player.getChargeP());
            healthBar.setSize(player.getHealthP());

        }

    }
}
