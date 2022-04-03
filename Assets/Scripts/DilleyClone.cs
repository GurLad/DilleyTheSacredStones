using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilleyClone : MonoBehaviour
{
    public float Cooldown;
    public AudioClip Snap;
    private AudioSource source;
    private float count;
    private static int totalCount;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (totalCount > 0)
        {
            source.PlayOneShot(Snap);
        }
        totalCount++;
        if (totalCount == 128)
        {
            SceneLoader.LoadScene("Win");
        }
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= Cooldown)
        {
            count -= Cooldown;
            Instantiate(gameObject);
        }
    }
}
