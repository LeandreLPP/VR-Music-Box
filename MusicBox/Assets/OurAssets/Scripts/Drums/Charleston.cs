using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charleston : MonoBehaviour {

    AudioSource source;
    public AudioClip open;
    public AudioClip close;
    bool isOpen;

    
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        isOpen = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetIsOpen(bool b)
    {
        isOpen = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (isOpen)
            source.clip = open;
        else
            source.clip = close;

        if (other.attachedRigidbody.velocity.y < 0)
        {
            source.Play();
        }

    }

}
