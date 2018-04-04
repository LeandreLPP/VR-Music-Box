using UnityEngine;

public class PhotonSpawner : StackingSpawner
{
    public override void HandleNewNote(NoteObject noteObject, Note note)
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
            n.GetComponent<PhotonView>().RPC("RemoveToSpwaner", PhotonTargets.OthersBuffered,0);
            PhotonView photonView = n.GetComponent<PhotonView>();
            if (!photonView.isMine)
                photonView.GetComponent<PhotonNote>().TransferOwnership();
            PhotonNetwork.Destroy(photonView);
        }
        noteObject.GetComponent<PhotonView>().RPC("AddToSpwaner", PhotonTargets.OthersBuffered);
        NotesHold.Add(noteObject);
    }

    protected override void Update()
    {
        var railPos = -transform.right * decalage / 2 * NotesHold.Count;
        rail.transform.localPosition = Vector3.MoveTowards(rail.transform.localPosition, railPos, speed * Time.deltaTime);

        var copyList = NotesHold.ToArray();
        foreach (var n in copyList)
            if (n.IsGrabbed)
            {
                n.GetComponent<PhotonView>().RPC("RemoveToSpwaner", PhotonTargets.OthersBuffered, NotesHold.IndexOf(n));
                NotesHold.Remove(n);
            }


        for (int i = 0; i < NotesHold.Count; i++)
        {
            var notePos = transform.right * decalage * i;
            NotesHold[i].transform.localPosition = Vector3.MoveTowards(NotesHold[i].transform.localPosition, notePos, speed * Time.deltaTime);
        }
    }

    public void AddRPCNote(NoteObject noteObject)
    {
        noteObject.transform.position = PositionNote(NotesHold.Count);
        noteObject.transform.rotation = transform.rotation;
        noteObject.transform.SetParent(rail.transform);
        NotesHold.Add(noteObject);
    }
}
