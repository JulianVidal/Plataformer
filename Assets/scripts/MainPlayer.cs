using System;
using UnityEngine;

namespace scripts
{
    public class MainPlayer : MonoBehaviour
    {
        private Player _player;
        private Bar _chargeBar;

        private Bar _healthBar;

        //Forces that changes the movement of the player
        public float jumpForce = 200f;
        public float walkForce = 1000f;
        public float floatForce = 50f;
        /*public float dashForce = 500f;*/
        /*public int   jumpTime = 5;*/

        // Start is called before the first frame update
        private void Start()
        {

            _chargeBar = new Bar(transform.Find("/Main Camera").Find("ChargeBar").Find("Bar"));

            _healthBar = new Bar(transform.Find("/Main Camera").Find("HealthBar").Find("Bar"));

            _player = new Player(gameObject, GetComponent<Rigidbody2D>(), walkForce, jumpForce, floatForce);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _player.Update();
            _chargeBar.SetSize(_player.GetChargeP());
            _healthBar.SetSize(_player.GetHealthP());

        }

    }
}
