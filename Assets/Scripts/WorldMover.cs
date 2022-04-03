using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMover : MonoBehaviour
{
    public float Length;
    public float MoveBackSpeed;
    public bool NoPlayerController;
    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        pos += new Vector3(0, 0, MoveBackSpeed * Time.deltaTime);
        if (NoPlayerController)
        {
            if (pos.z > Length)
            {
                pos.z -= Length;
            }
        }
        else if (pos.z + Length < PlayerController.ZPos)
        {
            pos.z += Length;
        }
        transform.position = pos;
    }
}
