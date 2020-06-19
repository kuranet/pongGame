using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public AnimationClip anim;
    Animation animator;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        string name = "Name";
        animator.AddClip(anim, name);
        animator.Play(name);
        Debug.Log("I'm awake");
    }

}
