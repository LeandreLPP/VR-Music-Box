using UnityEngine;

/// <summary>
/// Basic detection of descending collision with a stick. Used by drums at the beginning of the project.
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

        var vel = other.GetComponent<ComputeVelocity>().Velocity;
        var rot = transform.rotation;
        vel = rot * vel;

        if (vel.y < 0)
        {
            source.Play();
        }

    }
}
