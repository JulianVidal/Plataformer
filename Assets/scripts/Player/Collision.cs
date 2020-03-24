using System;
using UnityEngine;

public class Collision
{
    //Direction of the ray (1 up, -1 down)
    private readonly Vector2 bottomDir = new Vector2(0, -1);
    private readonly Vector2 topDir = new Vector2(0, 1);

    //Direction of the ray (1 right, -1 left)
    private readonly Vector2 rightDir = new Vector2(1, 0);
    private readonly Vector2 leftDir = new Vector2(-1, 0);

    private float rayOriginOffset = 0.1f; // Offset stops the ray from colliding with the player
    private float rayDistance = 0.0001f;  // How far the ray travels

    public Vector3 _position;

    public Collision(Vector3 position)
    {
        _position = position;
    }

    private Hits getHits()
    {
        bool topLeftN, topRightN, bottomLeftS, bottomRightS, bottomLeftE, topLeftE, bottomRightW, topRightW;
        //Calculates the corners of the hit box
        Vector2 bottomLeft = new Vector2(_position.x - 0.5f + rayOriginOffset, _position.y - 1 - rayOriginOffset);
        Vector2 bottomRight = new Vector2(_position.x + 0.5f - rayOriginOffset, _position.y - 1 - rayOriginOffset);

        Vector2 topLeft = new Vector2(_position.x - 0.5f + rayOriginOffset, _position.y + 1 + rayOriginOffset);
        Vector2 topRight = new Vector2(_position.x + 0.5f - rayOriginOffset, _position.y + 1 + rayOriginOffset);

        Vector2 midLeft = new Vector2(_position.x - 0.5f + rayOriginOffset, _position.y + rayOriginOffset);
        Vector2 midRight = new Vector2(_position.x + 0.5f - rayOriginOffset, _position.y + rayOriginOffset);

        // Casts ray and returns collisions
        RaycastHit2D hitTopLeftN = CastRay(topLeft, topDir);
        RaycastHit2D hitTopRightN = CastRay(topRight, topDir);

        RaycastHit2D hitBottomLeftS = CastRay(bottomLeft, bottomDir);
        RaycastHit2D hitBottomRightS = CastRay(bottomRight, bottomDir);

        // Casts ray and returns collisions
        RaycastHit2D hitBottomLeftE = CastRay(bottomLeft, leftDir);
        RaycastHit2D hitTopLeftE = CastRay(topLeft, leftDir);

        RaycastHit2D hitBottomRightW = CastRay(bottomRight, rightDir);
        RaycastHit2D hitTopRightW = CastRay(topRight, rightDir);

        if (hitTopLeftN.collider)
        {
            topLeftN = true;
        }
        else
        {
            topLeftN = false;
        }

        if (hitTopRightN.collider)
        {
            topRightN = true;
        }
        else
        {
            topRightN = false;
        }

        if (hitBottomLeftS.collider)
        {
            bottomLeftS = true;
        }
        else
        {
            bottomLeftS = false;
        }

        if (hitBottomRightS.collider)
        {
            bottomRightS = true;
        }
        else
        {
            bottomRightS = false;
        }

        if (hitBottomLeftE.collider)
        {
            bottomLeftE = true;
        }
        else
        {
            bottomLeftE = false;
        }

        if (hitTopLeftE.collider)
        {
            topLeftE = true;
        }
        else
        {
            topLeftE = false;
        }

        if (hitBottomRightW.collider)
        {
            bottomRightW = true;
        }
        else
        {
            bottomRightW = false;
        }

        if (hitTopRightW.collider)
        {
            topRightW = true;
        }
        else
        {
            topRightW = false;
        }

        return new Hits(topLeftN, topRightN, bottomLeftS, bottomRightS, bottomLeftE, topLeftE, bottomRightW, topRightW);
    }

    private RaycastHit2D CastRay(Vector2 origin, Vector2 direction)
    {

        Debug.DrawRay(origin, direction, Color.red, 0f, false);

        return Physics2D.Raycast(origin, direction, rayDistance, LayerMask.GetMask("Ground"));

    }

    public bool checkGround()
    {
        Hits hits = getHits();

        // Lets the player jump if there is a collision with the ground
        if (hits._hitBottomLeftS || hits._hitBottomRightS)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool checkRoof()
    {
        Hits hits = getHits();

        if (hits._hitTopLeftN || hits._hitTopRightN)
        {

            return true;

        }
        else
        {
            return false;
        }

    }

    public void update(Vector3 position)
    {
        _position = position;
    }
}