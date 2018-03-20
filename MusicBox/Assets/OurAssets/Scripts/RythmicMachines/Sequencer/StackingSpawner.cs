using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackingSpawner : SequencerNoteSpawner
{
    public GameObject rail;
    public float decalage = 0.25f;
    public float speed = 1f;
    public int maxNotes = 10;


    public List<NoteObject> NotesHold { get; set; }

    private void Start()
    {
        NotesHold = new List<NoteObject>();
    }

    public override void SpawnNote(NoteObject noteObject, NoteSound note)
    {
        noteObject.transform.position = PositionNote(NotesHold.Count);
        noteObject.transform.rotation = transform.rotation;
        noteObject.transform.SetParent(rail.transform);
        noteObject.note = note;
        while (NotesHold.Count >= maxNotes)
        {
            NoteObject n = NotesHold[0];
            NotesHold.RemoveAt(0);
            n.gameObject.SetActive(false);
            Destroy(n.GetComponent<PhotonView>());
        }
        NotesHold.Add(noteObject);
    }

    protected Vector3 PositionNote(int index)
    {
        return rail.transform.position + rail.transform.right * index * decalage;
    }

    protected virtual void Update()
    {
        var railPos = -transform.right * decalage / 2 * NotesHold.Count;
        rail.transform.localPosition = Vector3.MoveTowards(rail.transform.localPosition, railPos, speed * Time.deltaTime);

        var copyList = NotesHold.ToArray();
        foreach (var n in copyList)
            if (n.IsGrabbed)
                NotesHold.Remove(n);

        for(int i = 0; i< NotesHold.Count; i++)
        {
            var notePos = transform.right * decalage * i;
            NotesHold[i].transform.localPosition = Vector3.MoveTowards(NotesHold[i].transform.localPosition, notePos, speed * Time.deltaTime);
        }
    }
}
