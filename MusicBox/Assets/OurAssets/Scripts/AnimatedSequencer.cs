using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSequencer : Sequencer {

    public SequencerStep stepPrefab;
    public float distance = 7;
    public float height = 2;
    public int capacity = 5;

    private SequencerStep[] stepsVisu;

    public bool AddNoteObject(int step, NoteObject note)
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

    public bool RemoveNoteObject(NoteObject note)
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
        stepsVisu = new SequencerStep[steps];
        float angle = 360f / steps;

        for(int i = 0; i < steps; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i, Vector3.up);
            var pos = rot * (Vector3.forward * distance + Vector3.up * height);
            stepsVisu[i] = Instantiate<GameObject>(stepPrefab.gameObject, pos, rot, transform).GetComponent<SequencerStep>();
            stepsVisu[i].Initalize(capacity,i);
        }
    }

    public new void PlayNextStep()
    {
        base.PlayNextStep();
        int i = CurrentStep == 0 ? steps - 1 : CurrentStep - 1;
        stepsVisu[i].PlayStep();
    }


    public float tempo = 60f;
    private void Update()
    {
        Tempo = tempo;
    }
}
