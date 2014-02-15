using UnityEngine;
using System.Collections;

/// <summary>
/// Deletes the colliding object
/// </summary>
public class DeletionScript : MonoBehaviour {

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter(Collision collision) {
		Destroy(collision.gameObject);
	}
}