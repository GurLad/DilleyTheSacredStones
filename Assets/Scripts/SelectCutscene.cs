using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCutscene : MonoBehaviour
{
    public List<TCutscene> Cutscenes;

    private void Start()
    {
        Cutscenes[GameConsts.CurrentLevel].Activate();
    }

    private void Update()
    {
        if (Cutscenes[GameConsts.CurrentLevel].Done)
        {
            SceneLoader.LoadScene("Game");
            Destroy(this);
        }
    }
}
