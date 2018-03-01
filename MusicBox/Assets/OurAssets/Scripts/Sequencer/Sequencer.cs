using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sequencer : MonoBehaviour {

    #region Settings
    public int steps = 50;
    #endregion

    public virtual float Tempo
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
    
    protected void Start () {
        notes = new List<List<NoteSound>>();
        for (int i = 0; i < steps; i++)
            notes.Add(new List<NoteSound>());
	}

    public void PlayNextStep()
    {
        CurrentStep++;
        List<AudioSource> sources = new List<AudioSource>();
        foreach(var s in GetComponents<AudioSource>())
            //if(!s.isPlaying) // Commenting it cause sound but else it break Unity's sound system
                sources.Add(s);

        int nbSounds = notes[CurrentStep].Count;

        for (int i = sources.Count; i < nbSounds; i++)
            sources.Add(gameObject.AddComponent<AudioSource>());

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
