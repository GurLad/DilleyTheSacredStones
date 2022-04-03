using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : Trigger
{
    public string Name;

    public override void Activate()
    {
        SceneLoader.LoadScene(Name);
    }
}
