using UnityEngine;
using System.Collections;

public class VisualSequencerStep : MonoBehaviour
{
    public VisualSequencerToggle togglePrefab;
    public float spacement = 0.5f;

    public VisualSequencer VisualSequencer { get; private set; }
    public Sequencer Sequencer { get; private set; }
    public int StepNumber { get; private set; }

    #region Private fields
    private VisualSequencerToggle[] toggles;
    private bool initialized = false;
    #endregion

    public void Initialize(Sequencer sequencer, VisualSequencer vs, int stepNumber)
    {
        VisualSequencer = vs;
        Sequencer = sequencer;
        toggles = new VisualSequencerToggle[sequencer.StepSize];
        StepNumber = stepNumber;
        for (int i = 0; i < sequencer.StepSize; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, (i + 1) * spacement);
            toggles[i] = Instantiate<GameObject>(togglePrefab.gameObject, pos, transform.rotation, transform).GetComponent<VisualSequencerToggle>();
            toggles[i].Initialize(Sequencer, this, i, Sequencer.Partition[StepNumber,i]);
        }
        initialized = true;
    }

    public void UpdateNote()
    {
        foreach (var m in toggles)
            m.UpdateNote();
    }

    public void PlayStep()
    {
        if (!initialized)
            return;

        GetComponent<Animation>().Play();
        foreach (var t in toggles)
            t.PlayToogle();
    }
}
