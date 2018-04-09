using UnityEngine;

public class Vibraphone : MonoBehaviour {

    protected AudioSource source;
    protected Animator animator;

    void Start() {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.GetComponent<ComputeVelocity>().Velocity.y < 0)
        {
            source.Play();
            animator.SetTrigger("play");
        }
    }
}
