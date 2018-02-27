using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour {

    private bool grabbed;
    public bool Grabbed
    {
        get
        {
            return grabbed;
        }

        set
        {
            if(value)
            {
                if (entered)
                    grabbing = entered;
                grabbed = true;
                OnGrabbed();
            }
            else
            {
                grabbing = null;
                grabbed = false;
                OnRelease();
            }
        }
    }

    protected void OnRelease()
    { 
    }

    protected void OnGrabbed()
    {
    }

    Vector3 initialPosition;

    public GameObject indicator;

    Collider grabbing;
    Collider entered;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Grabbed)
        {
            indicator.SetActive(true);
            entered = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (grabbing == other)
            Grabbed = false;

        if (!Grabbed)
            indicator.SetActive(false);

        if (entered == other)
            entered = null;
    }

    void Update()
    {
        if (Grabbed)
            indicator.SetActive(false);

        if (Grabbed && entered == null)
            Grabbed = false;
    }
}
