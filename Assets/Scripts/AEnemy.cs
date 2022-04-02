using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    public GameObject Explosion;

    public abstract void Generate(Vector2Int coords);

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(Explosion).transform.position = transform.position;
        }
    }
}
