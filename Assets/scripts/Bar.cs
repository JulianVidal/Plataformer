using UnityEngine;

public class Bar
{
    private readonly Transform _bar;

    public Bar(Transform bar) {
        _bar = bar;
    }

    public void SetSize(float size)
    {
        _bar.localScale = new Vector3(size, 1f);

    }
}
