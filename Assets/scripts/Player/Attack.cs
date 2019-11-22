using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private GameObject _gb;
    public Attack(GameObject gameObject)
    {
        _gb = gameObject;
    }

    public void melee()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        else
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = false;

        }
    }
}
