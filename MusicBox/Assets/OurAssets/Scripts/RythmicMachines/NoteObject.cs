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

        set
        {
            receptacle = value;
        }
    }

    protected override void OnGrabbed()
    {
        base.OnGrabbed();
        var source = GetComponent<AudioSource>();
        source.clip = note.audioClip;
        source.volume = note.volume;
        source.Play();
        transform.SetParent(null);
    }

    protected override void OnRelease(AGrabber grabber)
    {
        base.OnRelease(grabber);

        var source = GetComponent<AudioSource>();
        source.Stop();

        if (Receptacle)
        {
            transform.SetParent(Receptacle.transform);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().velocity = grabber.Velocity;
            GetComponent<Rigidbody>().angularVelocity = grabber.AngularVelocity;
        }
    }
    
}
