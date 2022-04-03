using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TExternal : ContinuousTrigger
{
    public List<Trigger> Triggers;
    public override bool Done
    {
        get
        {
            return !TrackDone || Triggers.Find(a => a is ContinuousTrigger && !((ContinuousTrigger)a).Done) == null;
        }
        set
        {
            base.Done = value;
        }
    }

    public override void Activate()
    {
        Triggers.ForEach(a => a.Activate());
    }
}
