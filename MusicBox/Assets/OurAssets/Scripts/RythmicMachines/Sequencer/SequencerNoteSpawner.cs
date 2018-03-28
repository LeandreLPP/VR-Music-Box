using UnityEngine;
using System.Collections;

public abstract class SequencerNoteSpawner : MonoBehaviour
{
    public abstract void SpawnNote(NoteObject noteObject, NoteSound note);
}
