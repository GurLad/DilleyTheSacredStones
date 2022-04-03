using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShip : BasicShip
{
    private bool rotated;

    protected override void ProcessOffsets(Vector2Int coords, Vector3 realWorldCoords)
    {
        TargetOffset += realWorldCoords;
        StartOffset += realWorldCoords;
        OutsideOffset += realWorldCoords;
        transform.localEulerAngles = new Vector3(0, -90, 0);
    }

    protected override void ShootingStep(float percent)
    {
        transform.position = TargetOffset + new Vector3(0, 0, PlayerController.ZPos);
        if (!rotated)
        {
            if (spawnBeat == Conductor.SongPositionInBeats)
            {
                transform.localEulerAngles = new Vector3(0, -90 * (1 - Conductor.TimeSinceLastBeat), 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                spawnBeat = Conductor.SongPositionInBeats;
                rotated = true;
            }
        }
        else if (percent >= 1)
        {
            // Finish
            mode = Mode.Leaving;
            spawnBeat = Conductor.SongPositionInBeats;
            rotated = false;
        }
    }

    protected override void LeavingStep(float percent)
    {
        base.LeavingStep(percent);
        if (!rotated)
        {
            if (spawnBeat == Conductor.SongPositionInBeats)
            {
                transform.localEulerAngles = new Vector3(0, -90 * Conductor.TimeSinceLastBeat, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                rotated = true;
            }
        }
    }
}
