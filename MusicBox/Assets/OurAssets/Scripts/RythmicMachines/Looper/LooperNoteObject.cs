using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LooperNoteObject : BaseGrabable {

    public NoteSound note;

    private LooperNoteReceptacle receptacle;
    private float speed = 15f;

    public AnimatedLooper looper;

    private LooperNoteReceptacle potentialReceptacle;

    internal void SetReceptacle(LooperNoteReceptacle p)
    {
        receptacle = p;
        if (p == null)
            transform.SetParent(null);
        else
            transform.SetParent(p.transform);
        GetComponent<Rigidbody>().useGravity = (p == null);
    }

    private void Update()
    {
        if (receptacle != null && !IsGrabbed && transform.position != receptacle.transform.position)
            transform.position = Vector3.MoveTowards(transform.position, receptacle.transform.position, speed * Time.deltaTime);

        if (receptacle == null || transform.position == receptacle.transform.position)
            GetComponent<Collider>().enabled = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (potentialReceptacle && other.gameObject == potentialReceptacle.gameObject && IsGrabbed)
            potentialReceptacle = null;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (IsGrabbed && other.GetComponent<LooperNoteReceptacle>())// && (receptacle == null || other.gameObject != receptacle.gameObject))
            potentialReceptacle = other.GetComponent<LooperNoteReceptacle>();
        else if (other.tag == "Ground")
            Destroy(this);
    }

    protected override void OnGrabbed()
    {
        if(receptacle != null)
        {
            potentialReceptacle = receptacle;
            receptacle = null;
            looper.RemoveNoteObject(this);
        }
    }

    protected override void OnRelease(AGrabber grabber)
    {
        if (potentialReceptacle == null)
            GetComponent<Rigidbody>().useGravity = true;
        else
        {
            looper.RemoveNoteObject(this);
            if(!looper.AddNoteObject(potentialReceptacle.Step, this))
                GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
