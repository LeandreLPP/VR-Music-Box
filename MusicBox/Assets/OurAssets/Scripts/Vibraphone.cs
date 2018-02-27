using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibraphone : MonoBehaviour {

    // Use this for initialization

    AudioSource source;
    bool onTop;


    Animator animator;
    GameObject right;

    void Start() {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update () {

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
