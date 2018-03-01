using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteObject : BaseGrabable {

    public NoteSound note;

    private NoteReceptacle receptacle;
    private float speed = 15f;

    public AnimatedSequencer sequencer;

    private NoteReceptacle potentialReceptacle;

    internal void SetReceptacle(NoteReceptacle p)
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
        if (IsGrabbed && other.GetComponent<NoteReceptacle>())// && (receptacle == null || other.gameObject != receptacle.gameObject))
            potentialReceptacle = other.GetComponent<NoteReceptacle>();
        else if (other.tag == "Ground")
            Destroy(this);
    }

    protected override void OnGrabbed()
    {
        if(receptacle != null)
        {
            potentialReceptacle = receptacle;
            receptacle = null;
            sequencer.RemoveNoteObject(this);
        }
    }

    protected override void OnRelease()
    {
        if (potentialReceptacle == null)
            GetComponent<Rigidbody>().useGravity = true;
        else
        {
            sequencer.RemoveNoteObject(this);
            if(!sequencer.AddNoteObject(potentialReceptacle.Step, this))
                GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
