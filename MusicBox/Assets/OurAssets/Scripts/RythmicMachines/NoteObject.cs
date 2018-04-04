using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NoteObject : BaseGrabable
{
    public Note note;

    public Vector3 preferedEuler;

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
            if(!IsGrabbed && receptacle != null)
            {
                transform.SetParent(receptacle.transform);
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = preferedEuler;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }

    private void Start()
    {
        preferedEuler = transform.localEulerAngles;
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

    protected override void OnRelease(IGrabber grabber)
    {
        base.OnRelease(grabber);

        var source = GetComponent<AudioSource>();
        source.Stop();

        if (Receptacle)
        {
            transform.SetParent(Receptacle.transform);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = preferedEuler;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            transform.SetParent(null);
            GetComponent<Rigidbody>().useGravity = true;
            if (grabber != null)
            {
                GetComponent<Rigidbody>().velocity = grabber.Velocity;
                GetComponent<Rigidbody>().angularVelocity = grabber.AngularVelocity;
            }
        }
    }
    
}
