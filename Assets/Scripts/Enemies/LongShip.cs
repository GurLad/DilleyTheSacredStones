using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongShip : BasicShip
{
    public bool Vertical;

    protected override void ProcessOffsets(Vector2Int coords, Vector3 realWorldCoords)
    {
        Vector2 finalCoords = coords;
        if (Vertical)
        {
            finalCoords.y = Mathf.Min(coords.y, 0) + 0.5f;
            if (finalCoords.y < 0)
            {
                StartOffset.y *= -1;
                OutsideOffset.y *= -1;
            }
        }
        else
        {
            finalCoords.x = Mathf.Min(coords.x, 0) + 0.5f;
            if (finalCoords.x < 0)
            {
                StartOffset.x *= -1;
                OutsideOffset.x *= -1;
            }
        }
        realWorldCoords = new Vector3(finalCoords.x, finalCoords.y) * GameConsts.SHIP_SIZE;
        TargetOffset += realWorldCoords;
        StartOffset += realWorldCoords;
        OutsideOffset += realWorldCoords;
    }
}
