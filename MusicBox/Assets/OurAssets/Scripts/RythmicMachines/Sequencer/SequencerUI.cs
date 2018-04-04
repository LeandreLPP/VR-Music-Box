using UnityEngine;

[RequireComponent(typeof(Sequencer))]
public class SequencerUI : MonoBehaviour {

    public float distance = 6f;
    public float heightDistance = 0.5f;
    public SequencerStepUI stepPrefab;
    public SequencerNoteReceptacle receptaclePrefab;

    public GameObject root;

    private Sequencer sequencer;
    private SequencerStepUI[] stepsVisu;
    private SequencerNoteReceptacle[] receptacles;

    protected bool initialized;
    private bool paused;

    public virtual int Steps
    {
        get
        {
            return sequencer.Steps;
        }
        set
        {
            sequencer.Steps = value;
            Reinit();
        }
    }

    public virtual int StepSize
    {
        get
        {
            return sequencer.StepSize;
        }
        set
        {
            sequencer.StepSize = value;
            Reinit();
        }
    }

    public Sequencer Sequencer
    {
        get
        {
            return sequencer;
        }

        private set
        {
            sequencer = value;
        }
    }

    public SequencerStepUI[] StepsVisu
    {
        get
        {
            return stepsVisu;
        }

        private set
        {
            stepsVisu = value;
        }
    }

    public SequencerNoteReceptacle[] Receptacles
    {
        get
        {
            return receptacles;
        }

        private set
        {
            receptacles = value;
        }
    }

    protected NoteObject[] notes;

    public void SetNote(int height, NoteObject no)
    {
        notes[height] = no;
        sequencer.Notes[height] = no != null ? no.note : null;
        foreach (var s in StepsVisu)
            s.UpdateNote();
    }

    public NoteObject GetNote(int height)
    {
        return notes[height]; 
    }

    // Use this for initialization
    protected virtual void Start()
    {
        sequencer = GetComponent<Sequencer>();
        Initialize();
    }

    private void Reinit()
    {
        initialized = false;
        foreach (var s in StepsVisu)
            Destroy(s);
        Initialize();
    }

    public virtual void Initialize()
    {
        StepsVisu = new SequencerStepUI[Steps];
        notes = new NoteObject[StepSize];
        float angle = 360f / Steps;

        for (int i = 0; i < Steps; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i, Vector3.up);
            var pos = rot * (root.transform.forward * distance);
            StepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, root.transform).GetComponent<SequencerStepUI>();
            StepsVisu[i].Initialize(sequencer, this, i);
        }

        receptacles = new SequencerNoteReceptacle[StepSize];
        for (int i = 0; i < StepSize; i++)
        {
            var pos = root.transform.up * heightDistance * (i + 2);
            var rot = new Quaternion();
            receptacles[i] = Instantiate<GameObject>(receptaclePrefab.gameObject, pos, rot, root.transform).GetComponent<SequencerNoteReceptacle>();
            receptacles[i].Initialize(sequencer, this, i);
        }

        initialized = true;
    }

    public void PlayVisualNextStep()
    {
        if (!initialized || paused)
            return;
        sequencer.PlayNextStep();
        int i = sequencer.CurrentStep == Steps ? 0 : sequencer.CurrentStep;
        StepsVisu[i].PlayStep();
    }
}
