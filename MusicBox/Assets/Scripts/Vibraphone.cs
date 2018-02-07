using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibraphone : MonoBehaviour {

    // Use this for initialization

    AudioSource source;
    
    Animator animator;

    void Start () {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider c)
    {
        source.Play();
        animator.SetTrigger("play");
    }
}
