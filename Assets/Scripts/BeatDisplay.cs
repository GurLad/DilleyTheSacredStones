using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatDisplay : MonoBehaviour
{
    public Image BeatIcon;

    private void Update()
    {
        BeatIcon.color = new Color(1, 1 - Conductor.TimeSinceLastBeat / 2, 1 - Conductor.TimeSinceLastBeat / 2);
    }
}
