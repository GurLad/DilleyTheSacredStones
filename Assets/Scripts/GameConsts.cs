using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConsts
{
    public static readonly Vector2Int MAX_COORDS = new Vector2Int(1, 1);
    public static readonly Vector2Int MIN_COORDS = new Vector2Int(-1, -1);
    public const float SHIP_SIZE = 3.5f;
    public const float MIN_ACCURACY = 0.35f;

    public static int CurrentLevel = 0;
}
