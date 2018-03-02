using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class NoteObject : BaseGrabable
{
    public NoteSound note;

    private NoteReceptacle receptacle;
    public NoteReceptacle Receptacle
    {
        get
        {
            return receptacle;
        }

        private set
        {
            receptacle = value;
            if (receptacle)
            {
                transform.SetParent(receptacle.transform);
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
            }
            else transform.SetParent(null);
        }
    }

    private NoteReceptacle potentialReceptacle = null;

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
        var source = GetComponent<AudioSource>();
        source.clip = note.audioClip;
        source.volume = note.volume;
        potentialReceptacle = Receptacle;
        if (Receptacle)
            Receptacle.RemoveNote(this);
        Receptacle = null;
    }

    protected override void OnRelease()
    {
        base.OnRelease();
        if (potentialReceptacle && potentialReceptacle.SetNote(this))
            Receptacle = potentialReceptacle;
        else
            GetComponent<Rigidbody>().useGravity = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        var r = other.GetComponent<NoteReceptacle>();
        if (r && Receptacle == null)
            potentialReceptacle = r;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        var r = other.GetComponent<NoteReceptacle>();
        if (r && r == potentialReceptacle)
        {
            potentialReceptacle = null;
            Receptacle = null;
        }
    }
}
