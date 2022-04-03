using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSetColor : Trigger
{
    public Renderer Target;
    public Color Color;

    public override void Activate()
    {
        Target.material.color = Color;
    }
}
