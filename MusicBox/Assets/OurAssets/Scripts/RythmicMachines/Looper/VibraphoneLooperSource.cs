using UnityEngine;

public class VibraphoneLooperSource : LooperNoteSource {
    
    protected Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("HeadStick"))
            return;

        if (other.GetComponent<ComputeVelocity>().Velocity.y < 0)
        {
            Play();
            animator.SetTrigger("play");
        }
    }
}
