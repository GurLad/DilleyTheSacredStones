using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed;
    public Vector3 Direction;

    private void Update()
    {
        transform.Rotate(Direction * Speed * Time.deltaTime);
    }
}
