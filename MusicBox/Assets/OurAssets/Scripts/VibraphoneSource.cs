using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteSource))]
public class VibraphoneSource : MonoBehaviour {
    
    NoteSource source;
    bool onTop;
    
    Animator animator;
    GameObject right;

    void Start()
    {
        source = GetComponent<NoteSource>();
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.attachedRigidbody.velocity.y < 0)
        {
            source.Play();
            animator.SetTrigger("play");
        }
    }
}
