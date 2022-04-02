using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - Target.transform.position;
    }

    private void Update()
    {
        transform.position = offset + new Vector3(0, 0, Target.transform.position.z);
    }
}
