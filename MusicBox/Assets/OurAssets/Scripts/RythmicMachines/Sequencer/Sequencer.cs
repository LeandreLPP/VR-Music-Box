using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour {

    #region Settings
    public int steps = 50;
    public int maxSource = 20;
    public int stepSize = 5;
    public float tempo = 60f;
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
    
    public virtual int Steps
    {
        get
        {
            return steps;
        }
        set
        {
            steps = value;
            // TODO
        }
    }

    public virtual int StepSize
    {
        get
        {
            return stepSize;
        }
        set
        {
            stepSize = value;
            // TODO
        }
    }

    protected bool[,] partition;
    public virtual bool[,] Partition
    {
        get
        {
            return partition;
        }

        protected set
        {
            partition = value;
        }
    }
    
    private Note[] notes;
    public virtual Note[] Notes
    {
        get
        {
            return notes;
        }

        protected set
        {
            notes = value;
        }
    }

    public int currentStep;
    public int CurrentStep
    {
        get
        {
            return currentStep;
        }
        protected set
        {
            currentStep = value % Steps;
        }
    }

    private Queue<AudioSource> queueSource;

    void Awake()
    {
        Partition = new bool[steps, stepSize];
        Notes = new Note[stepSize];
        Tempo = tempo;
        queueSource = new Queue<AudioSource>();
    }

    private void Update()
    {
        Tempo = tempo;
    }

    public void PlayNextStep()
    {
        CurrentStep++;

        AudioSource[] presentSources = GetComponents<AudioSource>();
        Queue<AudioSource> freeSources = new Queue<AudioSource>();

        foreach (var s in presentSources)
            if (!s.isPlaying) // Don't interrupt playing sound
                freeSources.Enqueue(s);

        int added = 0;
        for (int i = 0; i < stepSize; i++)
            if (partition[CurrentStep, i] && Notes[i] != null)
            {
                AudioSource source;
                if (freeSources.Count > 0)
                {
                    source = freeSources.Dequeue();
                }
                else
                {
                    if (presentSources.Length + added < maxSource)
                    {
                        source = gameObject.AddComponent<AudioSource>();
                        added++;
                    }
                    else
                        source = queueSource.Dequeue();
                }
                
                source.clip = Notes[i].audioClip;
                source.volume = Notes[i].volume;
                source.Play();
                queueSource.Enqueue(source);
            }
       
    }
}
