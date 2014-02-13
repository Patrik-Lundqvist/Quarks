using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Vector3 savedVelocity;
	private Vector3 savedAngularVelocity;

	public Material ballMaterial;
	public Material ballMaterialDeActived;

	public AudioClip BounceWall;

	public PhysicMaterial ballPhysMaterial;
	private bool m_Activated;

	public bool Activated
	{
		get { return m_Activated; }
		set 
		{ 

			if(value)
			{
				ActivateBall();
			}
			else
			{
				DisableBall();
			}

			m_Activated = value; 
		}
	}

	// Use this for initialization
	public virtual void Start () {
		if(Activated){
			renderer.material = ballMaterial;
		}
		collider.material = ballPhysMaterial;
	}

	public void DisableBall(){
		StopBall();
		renderer.material = ballMaterialDeActived;
		rigidbody.detectCollisions = false;
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,-0.5f);
	}

	public void ActivateBall(){
		StartBall();
		renderer.material = ballMaterial;
		rigidbody.detectCollisions = true;
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,0f);
	}
	
	public void StopBall()
	{
		savedVelocity = rigidbody.velocity;
		savedAngularVelocity = rigidbody.angularVelocity;
		rigidbody.isKinematic = true;
	}

	public void StartBall()
	{
		rigidbody.isKinematic = false;
		rigidbody.AddForce( savedVelocity, ForceMode.VelocityChange );
		rigidbody.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
	}

	public virtual void OnCollisionEnter (Collision collision) {

		
		if(collision.gameObject.tag == "ObstacleBall")
		{
			// The offset
			float audioClipOffset = 22.0f;
			
			// Get the velocity and apply offset
			float p = collision.relativeVelocity.magnitude / audioClipOffset;

			// Change the pitch
			audio.pitch = Mathf.Clamp( p, 0.5f, 1.1f);

			// Change the volume
			audio.volume = Mathf.Clamp( p, 0.2f, 1.5f); // p is clamped to sane values

			// Play audio clip
			audio.PlayOneShot(BounceWall);
		}

	}


	// Update is called once per frame
	public virtual void Update () {
		
	}


}
