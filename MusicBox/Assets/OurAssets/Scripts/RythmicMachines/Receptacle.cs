using UnityEngine;
using System.Collections;

public abstract class NoteReceptacle : MonoBehaviour
{
    public virtual NoteObject NoteHold { get; protected set; }

    public abstract bool CanReceive(NoteObject note);

    public abstract bool SetNote(NoteObject note);

    public abstract bool RemoveNote(NoteObject note);

    protected virtual void OnTriggerEnter(Collider other)
    {
        var noteObject = other.GetComponent<NoteObject>();
        if (noteObject && CanReceive(noteObject))
        {
            SetNote(noteObject);
            noteObject.Receptacle = this;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        var noteObject = other.GetComponent<NoteObject>();
        if (noteObject && noteObject == NoteHold)
        {
            RemoveNote(noteObject);
            if(noteObject.Receptacle == this)
                noteObject.Receptacle = null;
        }
    }
}
