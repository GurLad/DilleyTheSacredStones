using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCutscene : ContinuousTrigger
{
    public float WormSpeed;
    public float EatPos;
    public float EndPos;
    public Transform Worm;
    public GameObject Planet;
    public List<GameObject> Stone;
    public AudioClip Sound;
    public AudioClip SFX;
    private bool activated;

    private void Update()
    {
        if (activated)
        {
            Worm.position += new Vector3(0, 0, WormSpeed) * Time.deltaTime;
            if (Worm.position.z > EatPos && Planet.activeSelf)
            {
                Planet.SetActive(false);
                Stone[GameConsts.CurrentLevel].SetActive(true);
                SoundController.PlaySound(SFX);
            }
            else if (Worm.position.z > EndPos)
            {
                Done = true;
                Destroy(this);
            }
        }
    }

    public override void Activate()
    {
        activated = true;
        SoundController.PlaySound(Sound);
    }
}
