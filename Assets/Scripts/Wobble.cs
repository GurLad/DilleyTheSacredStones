using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    public float Rate;
    public float Strength;

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * Rate) * Strength);
    }
}
