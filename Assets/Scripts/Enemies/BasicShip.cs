using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConsts;

public class BasicShip : AEnemy
{
    protected enum Mode { ReachingPosition, Shooting, Leaving }

    public Vector3 StartOffset;
    public Vector3 TargetOffset;
    public Vector3 OutsideOffset;
    public int ReachingPosBeats;
    public int ProjectileChargeBeats;
    public int LeavingPosBeats;
    public MeshRenderer Renderer;
    [Header("Projectiles")]
    public GameObject ProjectileObject;
    public float ProjectileSpeed;
    public Vector3 ProjectileOffset;
    public int GlowMaterialID;
    public Color GlowMaterialColor = Color.red;
    protected int spawnBeat;
    protected Mode mode;

    public override void Generate(Vector2Int coords)
    {
        spawnBeat = Conductor.SongPositionInBeats;
        mode = Mode.ReachingPosition;
        Vector3 realWorldCoords = new Vector3(coords.x, coords.y) * SHIP_SIZE;
        ProcessOffsets(coords, realWorldCoords);
        transform.position = StartOffset + new Vector3(0, 0, PlayerController.ZPos);
    }

    private void Update()
    {
        int currentStepBeats = mode switch
        {
            Mode.ReachingPosition => ReachingPosBeats,
            Mode.Shooting => ProjectileChargeBeats,
            Mode.Leaving => LeavingPosBeats,
            _ => throw new System.Exception("Impossible")
        };
        float percent = Mathf.Min(1, ((Conductor.SongPositionInBeats - spawnBeat) + Conductor.TimeSinceLastBeat) / currentStepBeats);
        switch (mode)
        {
            case Mode.ReachingPosition:
                ReachingPositionStep(percent);
                break;
            case Mode.Shooting:
                ShootingStep(percent);
                break;
            case Mode.Leaving:
                LeavingStep(percent);
                break;
            default:
                break;
        }
    }

    protected virtual void ProcessOffsets(Vector2Int coords, Vector3 realWorldCoords)
    {
        if (coords.y < 0)
        {
            StartOffset.y *= -1;
            OutsideOffset.y *= -1;
        }
        if (coords.x < 0)
        {
            StartOffset.x *= -1;
            OutsideOffset.x *= -1;
        }
        TargetOffset += realWorldCoords;
        StartOffset += realWorldCoords;
        OutsideOffset += realWorldCoords;
        if (GlowMaterialID > 0)
        {
            Renderer.materials[GlowMaterialID] = Instantiate(Renderer.materials[GlowMaterialID]);
            Renderer.materials[GlowMaterialID].color = Color.black;
        }
    }

    protected virtual void ReachingPositionStep(float percent)
    {
        percent = Mathf.Sqrt(percent);
        transform.position = StartOffset * (1 - percent) + TargetOffset * percent;
        transform.position += new Vector3(0, 0, PlayerController.ZPos);
        if (percent >= 1)
        {
            mode = Mode.Shooting;
            spawnBeat = Conductor.SongPositionInBeats;
        }
    }

    protected virtual void ShootingStep(float percent)
    {
        transform.position = TargetOffset + new Vector3(0, 0, PlayerController.ZPos);
        if (ProjectileObject != null)
        {
            if (spawnBeat + ProjectileChargeBeats > Conductor.SongPositionInBeats)
            {
                Renderer.materials[GlowMaterialID].color = GlowMaterialColor * percent;
            }
            else
            {
                // Generate projectile
                GameObject projectile = Instantiate(ProjectileObject);
                projectile.transform.position = transform.position + ProjectileOffset;
                spawnBeat = Conductor.SongPositionInBeats;
                ProjectileObject = null;
                Renderer.materials[GlowMaterialID].color = Color.black;
            }
        }
        else if (Conductor.SongPositionInBeats > spawnBeat)
        {
            // Finish
            mode = Mode.Leaving;
            spawnBeat = Conductor.SongPositionInBeats;
        }
    }

    protected virtual void LeavingStep(float percent)
    {
        percent = Mathf.Pow(percent, 2);
        transform.position = TargetOffset * (1 - percent) + OutsideOffset * percent;
        transform.position += new Vector3(0, 0, PlayerController.ZPos);
        if (percent >= 1)
        {
            Destroy(gameObject);
        }
    }
}
