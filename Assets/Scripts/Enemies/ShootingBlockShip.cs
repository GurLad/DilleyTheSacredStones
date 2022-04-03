using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlockShip : BlockShip
{
    protected override void LeavingStep(float percent)
    {
        if (ProjectileObject != null)
        {
            // Generate projectile
            GameObject projectile = Instantiate(ProjectileObject);
            projectile.transform.position = transform.position + ProjectileOffset;
            spawnBeat = Conductor.SongPositionInBeats;
            ProjectileObject = null;
        }
        base.LeavingStep(percent);
    }
}
