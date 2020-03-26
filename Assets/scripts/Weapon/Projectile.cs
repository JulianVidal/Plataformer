using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player" || hitInfo.name == "Fireball(Clone)") return;
        if (hitInfo.tag == "Enemy") hitInfo.GetComponent<Enemy>().health--;
        Destroy(gameObject);
    }
}
