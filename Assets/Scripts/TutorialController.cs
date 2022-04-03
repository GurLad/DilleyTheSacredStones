using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public Conductor Conductor;
    public EnemySpawner EnemySpawner;
    public PlayerController PlayerController;
    public GameObject TutorialObject;

    private void Start()
    {
        EnemySpawner.enabled = false;
        PlayerController.enabled = false;
        if (GameConsts.CurrentLevel > 0)
        {
            Begin();
        }
    }

    public void Begin()
    {
        Conductor.PlaySong("Inevitable");
        Destroy(TutorialObject);
        EnemySpawner.enabled = true;
        PlayerController.enabled = true;
    }
}
