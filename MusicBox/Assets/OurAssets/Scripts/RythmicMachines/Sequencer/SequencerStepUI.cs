using UnityEngine;

public class SequencerStepUI : MonoBehaviour
{
    public SequencerToggleUI togglePrefab;
    public float spacement = 0.5f;

    public SequencerUI VisualSequencer { get; protected set; }
    public Sequencer Sequencer { get; protected set; }
    public int StepNumber { get; protected set; }

    #region Private fields
    private SequencerToggleUI[] toggles;
    private bool initialized = false;
    #endregion

    public SequencerToggleUI[] Toggles { get; set; }

    public virtual void Initialize(Sequencer sequencer, SequencerUI vs, int stepNumber)
    {
        VisualSequencer = vs;
        Sequencer = sequencer;
        Toggles = new SequencerToggleUI[sequencer.StepSize];
        StepNumber = stepNumber;
        for (int i = 0; i < sequencer.StepSize; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, (i + 1) * spacement);
            Toggles[i] = Instantiate<GameObject>(togglePrefab.gameObject, pos, transform.rotation, transform).GetComponent<SequencerToggleUI>();
            Toggles[i].Initialize(Sequencer, this, i, Sequencer.Partition[StepNumber,i]);
        }
        initialized = true;
    }

    public void UpdateNote()
    {
        foreach (var m in Toggles)
            m.UpdateNote();
    }

    public void PlayStep()
    {
        if (!initialized)
            return;

        GetComponent<Animation>().Play();
        foreach (var t in Toggles)
            t.PlayToogle();
    }
}
