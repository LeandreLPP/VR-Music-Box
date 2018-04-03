using UnityEngine;

public class PedalDetection : MonoBehaviour {

    // Use this for initialization
    AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Stick"))
            return;

         source.Play();
    }
}
