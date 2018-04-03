using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Looper : MonoBehaviour {

    #region Settings
    public int steps = 50;
    public int maxSource = 20;
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
    private Queue<AudioSource> queueSource;
    
    protected void Start () {
        notes = new List<List<NoteSound>>();
        queueSource = new Queue<AudioSource>();
        for (int i = 0; i < steps; i++)
            notes.Add(new List<NoteSound>());
	}

    public void PlayNextStep()
    {
        CurrentStep++;

        AudioSource[] presentSources = GetComponents<AudioSource>();
        List<AudioSource> sources = new List<AudioSource>();

        foreach (var s in presentSources)
            if(!s.isPlaying) // Don't interrupt playing sound
                sources.Add(s);

        int nbSounds = notes[CurrentStep].Count;
        int added = 0;
        for (int i = sources.Count; i < nbSounds; i++)
        {
            if (presentSources.Length + added < maxSource)
            {
                sources.Add(gameObject.AddComponent<AudioSource>());
                added++;
            }
            else
                sources.Add(queueSource.Dequeue());
        }

        for(int i = 0; i < notes[CurrentStep].Count; i++)
        {
            sources[i].clip = notes[CurrentStep][i].audioClip;
            sources[i].volume = notes[CurrentStep][i].volume;
            sources[i].Play();
            queueSource.Enqueue(sources[i]);
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
