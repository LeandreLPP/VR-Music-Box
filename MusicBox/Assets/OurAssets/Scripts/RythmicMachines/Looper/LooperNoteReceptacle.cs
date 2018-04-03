using UnityEngine;

public class LooperNoteReceptacle : MonoBehaviour {

    #region Settings
    public Material baseMaterial;
    public Material containsNoteMaterial;
    public Material allowInsertMaterial;
    #endregion

    #region Protected fields
    private bool initialized = false;
    private LooperStep step;
    #endregion

    private LooperNoteObject noteHold;
    public LooperNoteObject NoteHold
    {
        get
        {
            return noteHold;
        }
        set
        {
            noteHold = value;
            if(noteHold)
                GetComponent<MeshRenderer>().material = containsNoteMaterial;
            else
                GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }

    public bool Free
    {
        get
        {
            return NoteHold == null;
        }
    }

    public int Step { get { return step.StepNumber; } }

    public void SetSoundObject(LooperNoteObject note)
    {
        if (!initialized) return;

        NoteHold = note;
        note.SetReceptacle(this);
    }

    public void RemoveSoundObject()
    {
        if (!initialized) return;

        NoteHold.SetReceptacle(null);
        NoteHold = null;
    }

    public void Initialized(LooperStep step)
    {
        this.step = step;
        initialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var noteObject = other.GetComponent<LooperNoteObject>();
        if (noteObject != null)
        {
            if(step.HasFreeSpot)
                GetComponent<MeshRenderer>().material = allowInsertMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var noteObject = other.GetComponent<LooperNoteObject>();
        if (noteObject != null)
        {
            if(noteHold != null)
                GetComponent<MeshRenderer>().material = containsNoteMaterial;
            else
                GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }
}
