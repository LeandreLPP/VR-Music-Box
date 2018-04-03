/// <summary>
/// An interface denoting an object that can be grabbed by a <see cref="IGrabber"/> object.
/// </summary>
public interface IGrabable {
    
    bool IsGrabbed { get; }

    /// <summary>
    /// The <see cref="IGrabber"/> holding this object. <c>null</c> if this object is not grabbed.
    /// </summary>
    IGrabber Grabber { get; }

    /// <summary>
    /// Try grabbing this <see cref="IGrabable"/> with a <see cref="IGrabber"/>.
    /// </summary>
    /// <param name="grab">The <see cref="IGrabber"/> trying to grab this object.</param>
    /// <returns><c>True</c> if the grab is successful, <c>false</c> otherwise.</returns>
    bool TryGrab(IGrabber grab);

    /// <summary>
    /// Call this method when a <see cref="IGrabber"/> releases this <see cref="IGrabable"/>.
    /// </summary>
    /// <param name="grab">The <see cref="IGrabber"/> trying to release this object.</param>
    /// <returns><c>True</c> if the realease is successful (meaning the <see cref="IGrabber"/> specified was the one holding this <see cref="IGrabable" />)</returns>
    bool TryRelease(IGrabber grab);

    /// <summary>
    /// Check if a <see cref="IGrabber"/> can grab this <see cref="IGrabable"/>
    /// </summary>
    /// <param name="grabber">The <see cref="IGrabber"/> wanting to grab this <see cref="IGrabable"/>.</param>
    /// <returns><c>True</c> if this object allows the <see cref="IGrabber"/> to grab it.</returns>
    bool CanGrab(IGrabber grabber);
}
