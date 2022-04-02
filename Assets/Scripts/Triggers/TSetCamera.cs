using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSetCamera : Trigger
{
    public string Name;

    public override void Activate()
    {
        CameraHolder.SetCamera(Name);
    }
}
