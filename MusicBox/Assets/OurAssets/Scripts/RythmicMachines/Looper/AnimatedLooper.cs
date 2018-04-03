using UnityEngine;

public class AnimatedLooper : Looper {

    public LooperStep stepPrefab;
    public float distance = 7;
    public float height = 2;
    public int capacity = 5;
    public float tempo = 60f;

    private LooperStep[] stepsVisu;

    public bool AddNoteObject(int step, LooperNoteObject note)
    {
        if (step < 0 || step >= stepsVisu.Length)
            return false;

        if (stepsVisu[step].AddSoundObject(note))
        {
            AddNote(step, note.note);
            return true;
        }
        else return false;
    }

    public bool RemoveNoteObject(LooperNoteObject note)
    {
        foreach(var step in stepsVisu)
        {
            if(step.RemoveSoundObject(note))
            {
                RemoveNote(note.note);
                return true;
            }
        }
        return false;
    }

    private new void Start()
    {
        base.Start();
        stepsVisu = new LooperStep[steps];
        float angle = 360f / steps;

        for(int i = 0; i < steps; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i, Vector3.up);
            var pos = rot * (Vector3.forward * distance + Vector3.up * height);
            stepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, transform).GetComponent<LooperStep>();
            stepsVisu[i].Initalize(capacity,i);
        }

        Tempo = tempo;
    }

    public new void PlayNextStep()
    {
        base.PlayNextStep();
        int i = CurrentStep == 0 ? steps - 1 : CurrentStep - 1;
        stepsVisu[i].PlayStep();
    }
}
