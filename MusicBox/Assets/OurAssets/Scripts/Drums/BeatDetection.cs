using UnityEngine;

public class BeatDetection : MonoBehaviour {

    AudioSource source;
    
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	}


    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        Debug.Log(other.attachedRigidbody.velocity.y);
        Debug.Log(other.attachedRigidbody.velocity.x);
        Debug.Log(other.attachedRigidbody.velocity.z);
        if (other.attachedRigidbody.velocity.y < 0)
        {

            source.Play();
        }

    }
}
