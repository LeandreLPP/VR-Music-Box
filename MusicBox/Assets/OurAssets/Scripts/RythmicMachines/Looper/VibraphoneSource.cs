using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibraphoneSource : NoteSource {
    
    protected Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.GetComponent<ComputeVelocity>().velocity.y < 0)
        {
            Play();
            animator.SetTrigger("play");
        }
    }
}
