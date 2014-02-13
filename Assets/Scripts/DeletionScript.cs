using UnityEngine;
using System.Collections;

public class DeletionScript : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		Destroy(collision.gameObject);
	}
}