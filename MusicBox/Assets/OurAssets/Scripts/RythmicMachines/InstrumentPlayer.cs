using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPlayer : SequencerNoteSpawner {

    public GameObject[] instrumentsPrefabs;
    public GameObject rack;
    public GameObject target;
    public GameObject spawnPoint;

    public Button next;
    public Button previous;
    public Button play;
    public Text instrumentName;

    private NoteObject noteHeld;

    public float distance = 100f;

    private int index = 0;
    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            var i = Mathf.Min(instruments.Length-1, Mathf.Max(0,value));
            Debug.Log("New i = " + i);
            if (i != index)
            {
                instruments[i].SetActive(true);
                index = i;
            }
            next.interactable = index < instruments.Length - 1;
            previous.interactable = index > 0;
            instrumentName.text = instruments[index].gameObject.name;
        }
    }

    private GameObject[] instruments;


    // Use this for initialization
    void Start () {
        List<GameObject> list = new List<GameObject>();
        int i = 0;
        foreach(var go in instrumentsPrefabs)
        {
            list.Add(Instantiate(go, rack.transform.position + -rack.transform.right * i * distance, rack.transform.rotation, rack.transform));
            i++;
        }
        instruments = list.ToArray();
        play.interactable = instruments.Length > 0;
        Index = 0;
        rack.transform.position = transform.position + transform.right.normalized * Index * distance;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position + transform.right.normalized * Index * distance;
        rack.transform.position = Vector3.MoveTowards(rack.transform.position, pos, distance * 2f * Time.deltaTime);
        rack.transform.rotation = transform.rotation;

        if (rack.transform.localPosition == pos)
        {
            for (int i = 0; i < instruments.Length; i++)
                if (Index != i)
                    instruments[i].SetActive(false);
        }

        if (noteHeld != null && noteHeld.IsGrabbed)
            noteHeld = null;

        // Turn toward target
        if (target != null)
            transform.LookAt(target.transform.position);

        // Remove NoteHeld once grabbed
        if (noteHeld != null && noteHeld.IsGrabbed)
        {
            noteHeld.transform.SetParent(null);
            noteHeld = null;
        }
	}

    public void Next()
    {
        Debug.Log("Next");
        Index++;
    }

    public void Previous()
    {
        Debug.Log("Previous");
        Index--;
    }

    public void Play()
    {
        Debug.Log("Play");
        SequencerNoteSource source = instruments[Index].GetComponent<SequencerNoteSource>();
        if (source != null)
        {
            Debug.Log("Play Not Null");
            source.spawner = this;
            source.Play();
        }
    }

    public override void SpawnNote(NoteObject noteObject, NoteSound note)
    {
        noteObject.note = note;
        noteObject.transform.SetParent(spawnPoint.transform);
        noteObject.transform.localPosition = Vector3.zero;
        if(noteHeld != null)
            Destroy(noteHeld.gameObject);
        noteHeld = noteObject;
    }
}
