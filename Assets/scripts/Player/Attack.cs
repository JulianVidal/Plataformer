using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private GameObject _gb;

    private float meleeCoolDown = 5;
    private float meleeLast = 0;
    public Attack(GameObject gameObject)
    {
        _gb = gameObject;
    }

    public void melee()
    {
        if (Input.GetKey(KeyCode.Space) && (Time.time - meleeLast) >= meleeCoolDown)
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = true;
            meleeLast = Time.time;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = false;

        }
    }
}
