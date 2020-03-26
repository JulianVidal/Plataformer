using UnityEngine;

public class Collision
{
    //Direction of the ray (1 up, -1 down)
    private readonly Vector2 _bottomDir = new Vector2(0, -1);
    private readonly Vector2 _topDir = new Vector2(0, 1);

    //Direction of the ray (1 right, -1 left)
    private readonly Vector2 _rightDir = new Vector2(1, 0);
    private readonly Vector2 _leftDir = new Vector2(-1, 0);

    private const float RayOriginOffset = 0.1f; // Offset stops the ray from colliding with the player
    private const float RayDistance = 0.0001f;  // How far the ray travels

    private Vector3 _position;

    public Collision(Vector3 position)
    {
        _position = position;
    }

    public Hits GetHits()
    {
        //Calculates the corners of the hit box
        var bottomLeft = new Vector2(_position.x - 0.5f + RayOriginOffset, _position.y - 1 - RayOriginOffset);
        var bottomRight = new Vector2(_position.x + 0.5f - RayOriginOffset, _position.y - 1 - RayOriginOffset);

        var topLeft = new Vector2(_position.x - 0.5f + RayOriginOffset, _position.y + 1 + RayOriginOffset);
        var topRight = new Vector2(_position.x + 0.5f - RayOriginOffset, _position.y + 1 + RayOriginOffset);

        //Vector2 midLeft = new Vector2(_position.x - 0.5f + rayOriginOffset, _position.y + rayOriginOffset);
        //Vector2 midRight = new Vector2(_position.x + 0.5f - rayOriginOffset, _position.y + rayOriginOffset);

        // Casts ray and returns collisions
        var topLeftN = CastRay(topLeft, _topDir);
        var topRightN = CastRay(topRight, _topDir);

        var bottomLeftS = CastRay(bottomLeft, _bottomDir);
        var bottomRightS = CastRay(bottomRight, _bottomDir);

        // Casts ray and returns collisions
        var bottomLeftE = CastRay(bottomLeft, _leftDir);
        var topLeftE = CastRay(topLeft, _leftDir);

        var bottomRightW = CastRay(bottomRight, _rightDir);
        var topRightW = CastRay(topRight, _rightDir);

        return new Hits(
            topLeftN.collider,
            topRightN.collider,
            bottomLeftS.collider,
            bottomRightS.collider,
            bottomLeftE.collider,
            topLeftE.collider,
            bottomRightW.collider,
            topRightW.collider
                );
    }

    private static RaycastHit2D CastRay(Vector2 origin, Vector2 direction)
    {

        Debug.DrawRay(origin, direction, Color.red, 0f, false);

        return Physics2D.Raycast(origin, direction, RayDistance, LayerMask.GetMask("Ground"));

    }

    public bool CheckGround()
    {
        var hits = GetHits();

        // Lets the player jump if there is a collision with the ground
        return hits.HitBottomLeftS || hits.HitBottomRightS;
    }

    public bool CheckRoof()
    {
        var hits = GetHits();

 
        return hits.HitTopLeftN || hits.HitTopRightN;

    }

    public void Update(Vector3 position)
    {
        _position = position;
    }
}