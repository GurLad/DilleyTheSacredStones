using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConsts;

public class PlayerController : MonoBehaviour
{
    public static float ZPos { get => current.PlayerObject.transform.position.z; }
    public static Vector2Int Coords { get => current.coords; }

    public float ForwardSpeed;
    public float MoveSpeed;
    public float RotationSpeed;
    public Rigidbody PlayerObject;
    public Transform ShipModel;
    [Header("Projectiles")]
    public float ProjectileCooldown;
    public Rigidbody ProjectileObject;
    public float ProjectileRelativeSpeed;
    public Vector3 ProjectileOffset;
    private Vector2Int coords = Vector2Int.zero;
    // Move vars
    private Vector2Int movingToCoords = MAX_COORDS + Vector2Int.right;
    private bool moving { get => movingToCoords.x <= MAX_COORDS.x; }
    private float count;
    private Vector2Int lastInput;
    // Projectile vars
    private float cooldown;
    private static PlayerController current;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        MoveXY();
        MoveZ();
        Projectile();
    }

    private void MoveXY()
    {
        Vector2Int targetMove = new Vector2Int(Control.GetAxisIntDown(Control.Axis.X), Control.GetAxisIntDown(Control.Axis.Y));
        if (!moving)
        {
            if (targetMove != Vector2Int.zero || (targetMove = lastInput) != Vector2Int.zero)
            {
                Debug.Log("Target move: " + targetMove);
                Vector2Int targetPos = coords + targetMove;
                targetPos.Clamp(MIN_COORDS, MAX_COORDS);
                Debug.Log("Target pos: " + targetPos);
                if (targetPos != coords)
                {
                    movingToCoords = targetPos;
                    count = 0;
                }
            }
            lastInput = Vector2Int.zero;
        }
        else
        {
            if (targetMove != Vector2Int.zero)
            {
                lastInput = targetMove;
            }
            count += Time.deltaTime * MoveSpeed;
            if (count >= 1)
            {
                count = 1;
            }
            float percent = -(Mathf.Sin(count * Mathf.PI + Mathf.PI / 2)) / 2 + 0.5f;
            PlayerObject.MovePosition(new Vector3(SHIP_SIZE * (coords.x * (1 - percent) + movingToCoords.x * percent), SHIP_SIZE * (coords.y * (1 - percent) + movingToCoords.y * count), PlayerObject.transform.localPosition.z));
            Vector2Int diff = coords - movingToCoords;
            float smoothValue = Mathf.Sin(count * Mathf.PI);
            ShipModel.localEulerAngles = new Vector3(diff.y * smoothValue, -diff.x * smoothValue) * RotationSpeed;
            if (count >= 1)
            {
                coords = movingToCoords;
                movingToCoords = MAX_COORDS + Vector2Int.right;
                count = 0;
            }
        }
    }

    private void MoveZ()
    {
        PlayerObject.velocity = new Vector3(0, 0, ForwardSpeed);
    }

    private void Projectile()
    {
        if (!moving && Control.GetButtonDown(Control.CB.A) && cooldown <= 0)
        {
            Rigidbody newProjectile = Instantiate(ProjectileObject.gameObject).GetComponent<Rigidbody>();
            newProjectile.velocity = new Vector3(0, 0, ProjectileRelativeSpeed + ForwardSpeed);
            newProjectile.transform.position = PlayerObject.transform.position + ProjectileOffset;
            cooldown = ProjectileCooldown;
        }
        cooldown -= Time.deltaTime;
    }
}
