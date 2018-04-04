using UnityEngine;

public class Cello : MonoBehaviour {

	AudioSource source;
	public AudioClip clip;
	bool isPlaying;
	Material mat;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = clip;
		isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        source.Play();
        isPlaying = true;
    }

    void OnTriggerExit(Collider other)
    {

        if (isPlaying)
        {
            source.Stop();
            isPlaying = false;
        }
        
    }
}
