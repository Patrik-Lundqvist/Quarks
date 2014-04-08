using UnityEngine;

/// <summary>
/// Deletes the colliding object
/// </summary>
public class DeletionScript : MonoBehaviour
{
    /// <summary>
    /// Raises the collision enter event.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// Raises the trigger enter event.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}