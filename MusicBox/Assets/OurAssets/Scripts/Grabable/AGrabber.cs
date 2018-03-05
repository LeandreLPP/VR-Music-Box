using UnityEngine;
using System.Collections;

public abstract class AGrabber : MonoBehaviour
{
    public bool IsGrabbing { get; protected set; }

    public AGrabable GrabbedObject { get; protected set; }
    public abstract Vector3 Forward { get; }
}
