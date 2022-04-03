using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSetDilleyAnimation : Trigger
{
    public string Name;

    public override void Activate()
    {
        DilleyAnimationsController.SetAnimation(Name);
    }
}
