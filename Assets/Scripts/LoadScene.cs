using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public string Name;

    public void Click()
    {
        SceneLoader.LoadScene(Name);
    }
}
