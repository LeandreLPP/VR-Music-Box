using UnityEngine;

public abstract class AGrabable : MonoBehaviour {
    
    private bool isGrabbed;
    public bool IsGrabbed
    {
        get
        {
            return isGrabbed;
        }
        protected set
        {
            isGrabbed = value;
        }
    }
    
    private AGrabber grabber;
    public AGrabber Grabber
    {
        get
        {
            return grabber;
        }

        protected set
        {
            grabber = value;
        }
    }

    protected abstract void OnGrabbed();

    protected abstract void OnRelease(AGrabber grabber);

    public bool Grab(AGrabber grab)
    {
        if (!CanGrab(grab))
            return false;
        isGrabbed = true;
        grabber = grab;
        OnGrabbed();
        return true;
    }

    public bool Release(AGrabber grab)
    {
        if (grabber != grab)
            return false;

        isGrabbed = false;
        grabber = null;
        OnRelease(grab);
        return true;
    }

    public virtual bool CanGrab(AGrabber grabber)
    {
        return !IsGrabbed;
    }

    protected abstract void OnValidGrabberEnter(AGrabber grabber);

    protected abstract void OnInvalidGrabberEnter(AGrabber grabber);

    protected abstract void OnGrabberExit(AGrabber grabber);

    protected virtual void OnTriggerEnter(Collider other)
    {
        var grabber = other.GetComponent<AGrabber>();
        if (grabber)
        {
            if (CanGrab(grabber))
                OnValidGrabberEnter(grabber);
            else
                OnInvalidGrabberEnter(grabber);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        var grabber = other.GetComponent<AGrabber>();
        if (grabber)
            OnGrabberExit(grabber);
    }

    void Update()
    {
    }
}
