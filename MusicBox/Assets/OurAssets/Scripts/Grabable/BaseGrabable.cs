using UnityEngine;
using System.Collections;

public class BaseGrabable : AGrabable
{
    public GameObject indicator;

    protected override void OnGrabbed()
    {
        indicator.SetActive(false);
    }

    protected override void OnRelease(AGrabber grabber)
    {
    }

    protected override void OnValidGrabberEnter(AGrabber grabber)
    {
        indicator.SetActive(true);
    }

    protected override void OnGrabberExit(AGrabber grabber)
    {
        if (!IsGrabbed)
            indicator.SetActive(false);
    }

    protected override void OnInvalidGrabberEnter(AGrabber grabber)
    {
    }
}
