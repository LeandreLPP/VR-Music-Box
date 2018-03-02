using UnityEngine;
using System.Collections;

public abstract class NoteReceptacle : MonoBehaviour
{
    public abstract bool CanReceive(NoteObject note);

    public abstract bool SetNote(NoteObject note);

    public abstract bool RemoveNote(NoteObject note);
}
