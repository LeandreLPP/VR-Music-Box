using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerStep : MonoBehaviour {

    public NoteReceptacle receptaclePrefab;
    private NoteReceptacle[] receptacles;
    private int nextFreeSpot = 0;

    private bool initialized = false;

    public int StepNumber { get; internal set; }

    public void Initalize(int capacity, int stepNumber)
    {
        receptacles = new NoteReceptacle[capacity];
        for(int i = 0; i < capacity; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, (i + 1));
            receptacles[i] = Instantiate<GameObject>(receptaclePrefab.gameObject, pos, transform.rotation, transform).GetComponent<NoteReceptacle>();
            receptacles[i].Initialized(this);
        }
        StepNumber = stepNumber;
        initialized = true;
    }

    public void PlayStep()
    {
        GetComponent<Animation>().Play();
    }

    public bool AddSoundObject(NoteObject note)
    {
        if (!initialized || nextFreeSpot >= receptacles.Length)
            return false;

        receptacles[nextFreeSpot].SetSoundObject(note);
        nextFreeSpot++;
        return true;
    }

    public bool RemoveSoundObject(NoteObject note)
    {
        bool found = false;

        for(int i = 0; i<nextFreeSpot; i++)
        {
            if(!found && receptacles[i].noteHold == note)
                found = true;
            
            if(found)
            {
                if (i < nextFreeSpot - 1) receptacles[i].SetSoundObject(receptacles[i + 1].noteHold);
                else receptacles[i].RemoveSoundObject();
            }
        }

        return found;
    }

}
