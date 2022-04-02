using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    private static CameraHolder current;
    public List<CameraObject> Cameras;
    private CameraObject currentCamera;

    private void Awake()
    {
        current = this;
    }

    public static void SetCamera(string name)
    {
        current.currentCamera?.Camera.gameObject.SetActive(false);
        (current.currentCamera = current.Cameras.Find(a => a.Name.ToLower() == name.ToLower()))?.Camera.gameObject.SetActive(true);
    }
}

[System.Serializable]
public record CameraObject
{
    public string Name;
    public Camera Camera;
}