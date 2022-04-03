using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorByLevel : MonoBehaviour
{
    public List<Color> Colors;
    public Renderer Renderer;
    public Image Image;

    private void Start()
    {
        if (Renderer != null)
        {
            Renderer.material.color = Colors[GameConsts.CurrentLevel];
        }
        else
        {
            Image.color = Colors[GameConsts.CurrentLevel];
        }
    }
}
