using System;
using UnityEngine;


public class MainPlayer : MonoBehaviour
{
    public Player player;
    public Bar chargeBar;

    public Bar healthBar;

    //Forces that changes the movement of the player
    public float jumpForce = 200f;
    public float walkForce = 1000f;
    public float floatForce = 50f;

    public Transform firePoint;
    public GameObject fireBallPrefab;

    
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
        if (player.GetHealthP() < 0) return;
        player.Update();
        player.SetChargeP(player.GetChargeP() + (float)0.0001);
        chargeBar.SetSize(player.GetChargeP());
        healthBar.SetSize(player.GetHealthP());
        
        if (Input.GetKey(KeyCode.RightControl))
        {
            Shoot();
        }

    }
    
    void Shoot()
    {
        if (player.GetChargeP() > 0.05)
        {
            player.SetChargeP(player.GetChargeP() - (float)0.05);
            Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
        }
    }
}

