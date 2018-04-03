using UnityEngine;

/// <summary>
/// A basic implementation of the <see cref="IGrabable"/> interface.
/// </summary>
public class BaseGrabable : MonoBehaviour, IGrabable
{
    public GameObject indicator;
    protected bool isGrabbed;
    public virtual bool IsGrabbed
    {
        get
        {
            return isGrabbed;
        }
        set
        {
            isGrabbed = value;
        }
    }

    private IGrabber grabber;
    public IGrabber Grabber
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

    public bool TryGrab(IGrabber grab)
    {
        if (!CanGrab(grab))
            return false;
        IsGrabbed = true;
        grabber = grab;
        OnGrabbed();
        return true;
    }

    public bool TryRelease(IGrabber grab)
    {
        if (grabber != grab)
            return false;

        IsGrabbed = false;
        grabber = null;
        OnRelease(grab);
        return true;
    }

    public virtual bool CanGrab(IGrabber grabber)
    {
        return !IsGrabbed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var grabber = other.GetComponent<IGrabber>();
        if (grabber != null)
        {
            if (CanGrab(grabber))
                OnValidGrabberEnter(grabber);
            else
                OnInvalidGrabberEnter(grabber);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        var grabber = other.GetComponent<IGrabber>();
        if (grabber != null)
            OnGrabberExit(grabber);
    }

    protected virtual void OnGrabbed()
    {
        indicator.SetActive(false);
    }

    protected virtual void OnRelease(IGrabber grabber)
    {
    }

    protected virtual void OnValidGrabberEnter(IGrabber grabber)
    {
        indicator.SetActive(true);
    }

    protected virtual void OnGrabberExit(IGrabber grabber)
    {
        if (!IsGrabbed)
            indicator.SetActive(false);
    }

    protected virtual void OnInvalidGrabberEnter(IGrabber grabber)
    {
    }
}
