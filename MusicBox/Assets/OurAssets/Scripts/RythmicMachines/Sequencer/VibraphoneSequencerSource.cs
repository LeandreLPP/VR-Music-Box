using UnityEngine;
using System.Collections;

public class VibraphoneSequencerSource : SequencerNoteSource
{
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
        }
    }

    protected override void Play()
    {
        base.Play();
        animator.SetTrigger("play");
    }
}
