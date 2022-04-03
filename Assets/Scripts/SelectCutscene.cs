using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCutscene : MonoBehaviour
{
    public TCutscene Cutscene;

    private void Start()
    {
        Cutscene.Activate();
    }

    private void Update()
    {
        if (Cutscene.Done)
        {
            SceneLoader.LoadScene("G");
        }
    }
}
