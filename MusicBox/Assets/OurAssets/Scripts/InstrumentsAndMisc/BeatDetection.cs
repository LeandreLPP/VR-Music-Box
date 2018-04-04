using UnityEngine;

/// <summary>
/// Basic detection of descending collision with a stick. Used by drums and vibraphone at the beginning of the project.
/// </summary>
public class BeatDetection : MonoBehaviour {

    AudioSource source;
    
    void Start () {
        source = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;
        
        if (other.attachedRigidbody.velocity.y < 0)
        {
            source.Play();
        }

    }
}
