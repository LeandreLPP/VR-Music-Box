using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteObject : Grabable {

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
        if (receptacle == null) return;

        if (transform.position != receptacle.transform.position)
            transform.position = Vector3.MoveTowards(transform.position, receptacle.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == receptacle.gameObject && Grabbed)
            sequencer.RemoveNoteObject(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Grabbed && other.GetComponent<NoteReceptacle>() && (receptacle == null || other.gameObject != receptacle.gameObject))
            potentialReceptacle = other.GetComponent<NoteReceptacle>();
        else if (other.tag == "Ground")
            Destroy(this);
    }

    protected new void OnGrabbed()
    {

    }

    protected new void OnRelease()
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
