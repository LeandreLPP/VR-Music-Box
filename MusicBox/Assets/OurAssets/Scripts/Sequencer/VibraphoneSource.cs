using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteSource))]
public class VibraphoneSource : MonoBehaviour {
    
    protected NoteSource source;
    protected Animator animator;

    void Start()
    {
        source = GetComponent<NoteSource>();
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.GetComponent<ComputeVelocity>().velocity.y < 0)
        {
            source.Play();
            animator.SetTrigger("play");
        }
    }
}
