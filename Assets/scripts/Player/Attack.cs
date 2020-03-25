using UnityEngine;

public class Attack
{
    private readonly GameObject _gb;

    private const float MeleeCoolDown = 5;
    private float _meleeLast;
    
    public Attack(GameObject gameObject)
    {
        _gb = gameObject;
    }

    public void Melee()
    {
        if (Input.GetKey(KeyCode.Space) && (Time.time - _meleeLast) >= MeleeCoolDown)
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = true;
            _meleeLast = Time.time;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            _gb.GetComponent<CapsuleCollider2D>().enabled = false;

        }
    }
}
