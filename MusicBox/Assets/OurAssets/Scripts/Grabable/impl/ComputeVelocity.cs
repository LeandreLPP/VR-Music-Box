using UnityEngine;

/// <summary>
/// Allow an object that doesn't use Unity physics to display a Velocity to other scripts.
/// </summary>
public class ComputeVelocity : MonoBehaviour
{

    protected Vector3 lastPos;

    /// <summary>
    /// The Velocity of the object.
    /// </summary>
    public Vector3 Velocity { get; set; }

    void Start()
    {
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        Velocity = transform.position - lastPos;
        lastPos = transform.position;
    }
}
