using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar
{
    public ChargeBar(Transform bar) {
        _bar = bar;
    }

    private Transform _bar;

    // Start is called before the first frame update
    void Start()
    {
        _bar.localScale = new Vector3(1f, 1f);
    }

    public void setSize(float size)
    {
        _bar.localScale = new Vector3(size, 1f);

    }
}
