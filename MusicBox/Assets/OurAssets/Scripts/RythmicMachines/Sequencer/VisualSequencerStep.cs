using UnityEngine;

public class VisualSequencerStep : MonoBehaviour
{
    public VisualSequencerToggle togglePrefab;
    public float spacement = 0.5f;

    public VisualSequencer VisualSequencer { get; protected set; }
    public Sequencer Sequencer { get; protected set; }
    public int StepNumber { get; protected set; }

    #region Private fields
    private VisualSequencerToggle[] toggles;
    private bool initialized = false;
    #endregion

    public VisualSequencerToggle[] Toggles { get; set; }

    public virtual void Initialize(Sequencer sequencer, VisualSequencer vs, int stepNumber)
    {
        VisualSequencer = vs;
        Sequencer = sequencer;
        Toggles = new VisualSequencerToggle[sequencer.StepSize];
        StepNumber = stepNumber;
        for (int i = 0; i < sequencer.StepSize; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, (i + 1) * spacement);
            Toggles[i] = Instantiate<GameObject>(togglePrefab.gameObject, pos, transform.rotation, transform).GetComponent<VisualSequencerToggle>();
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
