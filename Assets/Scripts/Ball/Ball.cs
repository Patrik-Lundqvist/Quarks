using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for ball behaviour
/// </summary>
public abstract class Ball : MonoBehaviour {



	// Variables for storing ball state
	private Vector3 savedVelocity;
	private Vector3 savedAngularVelocity;

	// The different ball materials
	public Material ballMaterial;
	public Material ballMaterialDeActived;
	public Material ballMaterialInvulnerable;
    public Material ballMaterialFrozen;

	// Physical material of the ball
	public PhysicMaterial ballPhysMaterial;

	// Is the ball active
	private bool _isActivated;

	// If our ball can die
	public bool _isInvulnerable;

    private bool _isFrozen;

    private float _freezeTime;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Ball"/> is activated.
	/// </summary>
	/// <value><c>true</c> if is activated; otherwise, <c>false</c>.</value>
	public bool isActivated
	{
		get { return _isActivated; }
		set 
		{ 
			if(value)
			{
				// Make sure the ball is activated
				ActivateBall();
			}
			else
			{
				// Make sure the ball is disabled
				DisableBall();
			}

			// Set the value
			_isActivated = value; 
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this ball is invulnerable.
	/// </summary>
	/// <value><c>true</c> if is invulnerable; otherwise, <c>false</c>.</value>
	public bool isInvulnerable
	{
		get { return _isInvulnerable; }
		set 
		{ 
			if(value)
			{
				// Change the material
				renderer.material = ballMaterialInvulnerable;
			}
			else
			{
				// Change the material
				renderer.material = ballMaterial;
			}
			
			// Set the value
			_isInvulnerable = value; 
		}
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public virtual void Start () {
		if(isActivated){
			renderer.material = ballMaterial;
		}
		collider.material = ballPhysMaterial;
	}

	/// <summary>
	/// Disables the ball.
	/// </summary>
	public virtual void DisableBall(){

		// Stop the ball
		StopBall();

		// Change the material
		renderer.material = ballMaterialDeActived;

		// Disable physical collisions
		collider.isTrigger = true;



		// Move the ball back a bit
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,8f);
	}

	/// <summary>
	/// Activates the ball.
	/// </summary>
	public virtual void ActivateBall(){

		// Make the ball move again
		StartBall();

		// Change the material
		renderer.material = ballMaterial;

		// Activate physical collisions
		collider.isTrigger = false;

		// Move the ball back to the correct level
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,0f);
	}

	/// <summary>
	/// Stops the ball.
	/// </summary>
    public virtual void StopBall()
	{
        
		// Save the velocity
		savedVelocity = rigidbody.velocity;
		// Save the direction
		savedAngularVelocity = rigidbody.angularVelocity;

        rigidbody.velocity = new Vector3(0,0,0);
        rigidbody.angularVelocity = new Vector3(0, 0, 0);
        this.gameObject.rigidbody.isKinematic = true;
	}

	/// <summary>
	/// Starts the ball.
	/// </summary>
    public virtual void StartBall()
	{
        this.gameObject.rigidbody.isKinematic = false;
		// Add the saved velocity
		rigidbody.AddForce( savedVelocity, ForceMode.VelocityChange );
		// Add the saved direction
		rigidbody.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void OnCollisionEnter (Collision collision) {


		// If the ball is an obstacle ball, play audio clip
		if(collision.gameObject.tag == "ObstacleBall")
		{
			// The offset
			var audioClipOffset = 445.0f;

			// Get the velocity and apply offset
			var p = collision.relativeVelocity.magnitude / audioClipOffset;

			// Play audio clip
			new OTSound("BallHit").Pitch(Mathf.Clamp( p, 0.5f, 1.1f)).Volume(Mathf.Clamp( p, 0.2f, 1.5f));
		}

	}

    public virtual void Freeze(float duration)
    {
        if (isActivated)
        {
            _freezeTime = duration;
            if (!_isFrozen)
            {
                // Change the material
                renderer.material = ballMaterialFrozen;
                _isFrozen = true;
                StopBall();
                StartCoroutine(FreezeCoroutine());
            }
        }

    }

    private IEnumerator FreezeCoroutine()
    {


        while (_freezeTime > 0)
        {
            _freezeTime -= 0.5f;
            if (_freezeTime <= 0)
            {
                // Change the material
                renderer.material = ballMaterial;
                _isFrozen = false;
                StartBall();
            }


            yield return new WaitForSeconds(0.5f);
        }

    }

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public virtual void Update () {
		
	}
}
