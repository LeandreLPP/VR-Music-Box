using UnityEngine;
using UnityEngine.EventSystems;

public class GNote : NoteObject
{


    void Start()
    {
    }

    //Handle when we detect a click on a note
    public void ClickOnNote(BaseEventData data)
    {
        if (IsGrabbed)
            return;
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        if (note && Receptacle)
            SwapNotes(note);
        else if (note)
            RemoveNote();
        TryGrab(null);
            
    }

    protected override void OnGrabbed()
    {
#if UNITY_ANDROID
        if(Receptacle)
            Receptacle.gameObject.layer = LayerMask.NameToLayer("Default");
#endif

        base.OnGrabbed();
#if UNITY_ANDROID
        // get the Transform component of the pointer
        Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;
        // set the GameObject's parent to the pointer
        transform.SetParent(pointerTransform, true);

        // position it in the view
        transform.localPosition = new Vector3(0, 0, 0.25f);

        gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");
#endif
    }

    //Remove a holding note
    protected virtual void RemoveNote()
    {
        NoteObject note = GvrPointerInputModule.Pointer.PointerTransform.GetComponentInChildren<NoteObject>();
        note.transform.SetParent(null);
        Destroy(note.GetComponent<PhotonView>());
    }

    //Click on a note which is in a receptacle and holding in the same time a note. Swap notes.
   public void SwapNotes(NoteObject note)
    {
        NoteReceptacle tmp = Receptacle;
        Receptacle.SwapNote(note);
        TryGrab(null);
        tmp.gameObject.layer = LayerMask.NameToLayer("GvrCannotGrab");
    }
}
