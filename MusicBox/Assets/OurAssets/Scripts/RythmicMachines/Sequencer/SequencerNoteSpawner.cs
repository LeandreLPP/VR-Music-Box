﻿using UnityEngine;
using System.Collections;

public abstract class SequencerNoteSpawner : MonoBehaviour
{
    public abstract void SpawnNote(NoteObject noteObjectPrefab, NoteSound note);
}