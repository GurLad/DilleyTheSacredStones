using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Lifetime;
    public float ShrinkRate;
    public bool UseForward;
    public bool NoRigidBody;
    [HideInInspector]
    public float SpawnAccuracy;
    private float count;
    private Vector3 baseScale;

    private void Start()
    {
        if (!NoRigidBody)
        {
            GetComponent<Rigidbody>().velocity = (UseForward ? transform.up : Vector3.forward) * Speed;
        }
        baseScale = transform.localScale;
    }

    private void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
        {
            count += Time.deltaTime * ShrinkRate;
            if (count >= 1)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = baseScale * (1 - count);
            }
        }
    }
}
