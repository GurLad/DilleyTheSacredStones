using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRot : MonoBehaviour
{
    public Vector2 RotSpeedRange;
    private Vector3 rotDirection;

    void Start()
    {
        rotDirection = new Vector3(Random.Range(0.00001f, 1), Random.Range(0.00001f, 1), Random.Range(0.00001f, 1)).normalized * Random.Range(RotSpeedRange.x, RotSpeedRange.y);
    }

    private void Update()
    {
        transform.Rotate(rotDirection * Time.deltaTime);
    }
}
