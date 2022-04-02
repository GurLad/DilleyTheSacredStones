using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    private static CameraHolder current;
    public List<Camera> Cameras;
    private Camera currentCamera;

    private void Awake()
    {
        current = this;
        Cameras.ForEach(a => a.gameObject.SetActive(false));
    }

    public static void SetCamera(string name)
    {
        current.currentCamera?.gameObject.SetActive(false);
        (current.currentCamera = current.Cameras.Find(a => a.name.ToLower() == name.ToLower()))?.gameObject.SetActive(true);
    }
}