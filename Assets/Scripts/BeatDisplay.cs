using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatDisplay : MonoBehaviour
{
    public Image BeatIcon;
    public Image PlanetIcon;
    private Vector2 baseSizeDelta;
    private Color basePlanetColor;

    private void Start()
    {
        baseSizeDelta = BeatIcon.rectTransform.sizeDelta;
        basePlanetColor = PlanetIcon?.color ?? Color.white;
    }

    private void Update()
    {
        BeatIcon.color = new Color(basePlanetColor.r, basePlanetColor.g, basePlanetColor.b, Mathf.Max(0, 1 - Conductor.TimeSinceLastBeat * 2));
        BeatIcon.rectTransform.sizeDelta = baseSizeDelta * (1 + Conductor.TimeSinceLastBeat / 2);
        if (PlanetIcon != null)
        {
            PlanetIcon.color = basePlanetColor * Mathf.Max(1, 1.2f - Conductor.TimeSinceLastBeat);
        }
    }
}
