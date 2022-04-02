using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly Vector2Int MAX_COORDS = new Vector2Int(1, 1);
    private static readonly Vector2Int MIN_COORDS = new Vector2Int(-1, -1);

    public float ForwardSpeed;
    public float MoveSpeed;
    public float RotationSpeed;
    public float ShipSize;
    public Transform PlayerObject;
    public Transform ShipModel;
    private Vector2Int coords = Vector2Int.zero;
    // Move vars
    private Vector2Int movingToCoords = MAX_COORDS + Vector2Int.right;
    private bool moving { get => movingToCoords.x <= MAX_COORDS.x; }
    private float count;
    private Vector2Int lastInput;

    private void Update()
    {
        MoveXY();
        MoveZ();
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
            PlayerObject.transform.localPosition = new Vector3(ShipSize * (coords.x * (1 - count) + movingToCoords.x * count), ShipSize * (coords.y * (1 - count) + movingToCoords.y * count), PlayerObject.transform.localPosition.z);
            Vector2Int diff = coords - movingToCoords;
            ShipModel.localEulerAngles = new Vector3(diff.y * Mathf.Sin(count * Mathf.PI), -diff.x * Mathf.Sin(count * Mathf.PI)) * RotationSpeed;
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
        PlayerObject.transform.localPosition += new Vector3(0, 0, ForwardSpeed * Time.deltaTime);
    }
}
