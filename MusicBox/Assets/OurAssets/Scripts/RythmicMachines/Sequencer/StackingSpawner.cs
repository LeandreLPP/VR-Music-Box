using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackingSpawner : SequencerNoteSpawner
{
    public GameObject rail;
    public float decalage = 0.25f;
    public float speed = 1f;
    public int maxNotes = 10;

    protected List<NoteObject> notesHold;

    private void Start()
    {
        notesHold = new List<NoteObject>();
    }

    public override void SpawnNote(NoteObject noteObject, NoteSound note)
    {
        Debug.Log("spawner");
        noteObject.transform.position = PositionNote(notesHold.Count);
        noteObject.transform.rotation = transform.rotation;
        noteObject.transform.SetParent(rail.transform);
        noteObject.note = note;
        while (notesHold.Count >= maxNotes)
        {
            NoteObject n = notesHold[0];
            notesHold.RemoveAt(0);
            n.gameObject.SetActive(false);
            Destroy(n);
        }
        notesHold.Add(noteObject);
    }

    protected Vector3 PositionNote(int index)
    {
        return rail.transform.position + rail.transform.right * index * decalage;
    }

    private void Update()
    {
        var railPos = -transform.right * decalage / 2 * notesHold.Count;
        rail.transform.localPosition = Vector3.MoveTowards(rail.transform.localPosition, railPos, speed * Time.deltaTime);

        var copyList = notesHold.ToArray();
        foreach (var n in copyList)
            if (n.IsGrabbed)
                notesHold.Remove(n);

        for(int i = 0; i<notesHold.Count; i++)
        {
            var notePos = transform.right * decalage * i;
            notesHold[i].transform.localPosition = Vector3.MoveTowards(notesHold[i].transform.localPosition, notePos, speed * Time.deltaTime);
        }
    }
}
