using UnityEngine;

public class ReturnBack : BaseGrabable {
    
    public Vector3 originalPos;
    public Quaternion originalRot;

    void Awake ()
    {
        IsGrabbed = false;
        originalRot = transform.rotation;
        originalPos = transform.position;
    }
	
	void Update () {
        if (IsGrabbed)
            return;

        if (!originalPos.Equals(transform.position))
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime);

        if (!originalRot.Equals(transform.localRotation))
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRot, Time.deltaTime);
    }
}
