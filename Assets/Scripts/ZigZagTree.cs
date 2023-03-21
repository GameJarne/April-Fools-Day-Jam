using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagTree : MonoBehaviour, Interactable
{
    private const string SHAKING_ANIM_NAME = "Shaking";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        animator.Play(SHAKING_ANIM_NAME);
    }
}
