using System;
using UnityEngine;

public class CatAnimController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    void Start()
    {
         animator = GetComponent<Animator>();
        GameEvents.OnSoundPlay += SoundPlay;
    }


    void SoundPlay(bool status)
    {
        animator.SetBool("Speaking", status);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        GameEvents.OnSoundPlay -= SoundPlay;
    }
}
