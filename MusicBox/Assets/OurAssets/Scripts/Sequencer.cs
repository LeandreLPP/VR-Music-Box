using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sequencer : MonoBehaviour {

    public int steps = 50;

    public float Tempo
    {
        get
        {
            return GetComponent<Animator>().GetFloat("tempoMultiplier");
        }
        set
        {
            GetComponent<Animator>().SetFloat("tempoMultiplier", value);
        }
    }

    private int currentStep;
    public int CurrentStep
    {
        get
        {
            return currentStep;
        }
        protected set
        {
            currentStep = value % steps;
        }
    }

    private List<List<NoteSound>> notes;

    // Use this for initialization
    protected void Start () {
        Debug.Log("Start called");
        notes = new List<List<NoteSound>>();
        for (int i = 0; i < steps; i++)
            notes.Add(new List<NoteSound>());
        //CurrentStep = -1;
	}

    public void PlayNextStep()
    {
        CurrentStep++;
        AudioSource[] sources = GetComponents<AudioSource>();
        int nbSounds = notes[CurrentStep].Count;
        bool upgraded = false;
        for (int i = sources.Length; i < nbSounds; i++)
        {
            gameObject.AddComponent<AudioSource>();
            upgraded = true;
        }
        if(upgraded)
            sources = GetComponents<AudioSource>();
        for(int i = 0; i < notes[CurrentStep].Count; i++)
        {
            sources[i].clip = notes[CurrentStep][i].audioClip;
            sources[i].volume = notes[CurrentStep][i].volume;
            sources[i].Play();
        }
    }
	
    public void AddNote(int step, NoteSound note)
    {
        notes[step].Add(note);
    }

    public void RemoveNote(NoteSound note)
    {
        foreach (var l in notes)
            if (l.Contains(note))
                l.Remove(note);
    }
}
