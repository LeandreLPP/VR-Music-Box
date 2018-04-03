using UnityEngine;

/// <summary>
/// An interface denoting an object capable of grabbing a <see cref="IGrabable"/>
/// </summary>
public interface IGrabber
{
    /// <summary>
    /// Is this <see cref="IGrabber"/> holding a <see cref="IGrabable"/> currently?
    /// </summary>
    bool IsGrabbing { get; }

    /// <summary>
    /// The <see cref="IGrabable"/> this <see cref="IGrabber"/> is holding. <c>Null</c> if no <see cref="IGrabable"/> is being held.
    /// </summary>
    IGrabable GrabbedObject { get; }
    
    Vector3 Forward { get; }
    Vector3 AngularVelocity { get; }
    Vector3 Velocity { get; }
}
