using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConsts;

public class BasicShip : AEnemy
{
    private enum Mode { ReachingPosition, Shooting, Leaving }

    public Vector3 StartOffset;
    public Vector3 TargetOffset;
    public int ReachingPosBeats;
    private int spawnBeat;
    private Mode mode;

    public override void Generate(Vector2Int coords)
    {
        spawnBeat = Conductor.SongPositionInBeats;
        mode = Mode.ReachingPosition;
        Vector3 realWorldCoords = new Vector3(coords.x, coords.y) * SHIP_SIZE;
        TargetOffset += realWorldCoords;
        if (coords.y < 0)
        {
            StartOffset.y *= -1;
        }
        if (coords.x < 0)
        {
            StartOffset.x *= -1;
        }
        StartOffset += realWorldCoords;
    }

    private void Update()
    {
        switch (mode)
        {
            case Mode.ReachingPosition:
                float percent = Mathf.Min(1, ((Conductor.SongPositionInBeats - spawnBeat) + Conductor.TimeSinceLastBeat) / ReachingPosBeats);
                percent = Mathf.Sqrt(percent);
                transform.position = StartOffset * (1 - percent) + TargetOffset * percent;
                transform.position += new Vector3(0, 0, PlayerController.ZPos);
                if (percent >= 1)
                {
                    mode = Mode.Shooting;
                    spawnBeat = Conductor.SongPositionInBeats;
                }
                break;
            case Mode.Shooting:
                transform.position = TargetOffset + new Vector3(0, 0, PlayerController.ZPos);
                break;
            case Mode.Leaving:
                break;
            default:
                break;
        }
    }
}
