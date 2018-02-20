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
            }
            else
            {
                grabbing = null;
                grabbed = false;
            }
        }
    }

    Vector3 initialPosition;

    public GameObject indicator;

    Collider grabbing;
    Collider entered;

    void Start()
    {
        initialPosition = transform.position;
        /*for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).tag == "Indicator")
                indicator = transform.GetChild(i).gameObject;*/
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
