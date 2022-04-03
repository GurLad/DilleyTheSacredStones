using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilleyAnimationsController : MonoBehaviour
{
    private static DilleyAnimationsController current;
    public List<AdvancedAnimation> Animations;
    private AdvancedAnimation currentAnimation;

    private void Awake()
    {
        current = this;
        Animations.ForEach(a => a.Active = false);
    }

    public static void SetAnimation(string name)
    {
        current.currentAnimation?.Deactivate();
        (current.currentAnimation = current.Animations.Find(a => a.name.ToLower() == name.ToLower()))?.Activate(true);
    }
}
