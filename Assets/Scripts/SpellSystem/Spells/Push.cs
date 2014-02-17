using UnityEngine;
using System.Collections;

/// <summary>
/// A spell that pushes enemies away from the player ball
/// </summary>
public class Push : Spell {

	Vector2 startSize;
	Vector2 endSize;
	
	float spellSize;
	float moveTime = 0.0f;
	
	OTSprite sprite;     

	/// <summary>
	/// Constructor
	/// </summary>
	public Push()
	{
		// Set the powercost of the spell
		powerCost = 1.0f;
		// Set the sound clip
		soundClip = "Push";
		// Set the name of the spell
		spellName = "Push";

		isSelfCast = true;
	}

	/// <summary>
	/// Find all the surrounding targets
	/// </summary>
	public override void Precast()
	{
		spellSize = 300 / caster.transform.localScale.x;

		// get this star's sprite class
		sprite = GetComponent<OTSprite>();

		sprite.size = new Vector2(0,0);
		startSize = sprite.size;
		endSize = new Vector2(spellSize, spellSize);

		// Make the ball invulnerable
		caster.isInvulnerable = true;

		base.Precast();
	}

	/// <summary>
	/// Move all the surround targets
	/// </summary>
	public override void Casting()
	{
		moveTime += Time.deltaTime * 3f;
		
		sprite.size = Vector2.Lerp (startSize, endSize, moveTime);



		if(sprite.size.x >= spellSize)
		{
			EndCasting();
			caster.isInvulnerable = false;
		}

		base.Casting();

	}

	/// <summary>
	/// If we trigger any of the trigger colliders
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)	
	{	
		// Get the object name of the object which we collided with
		string hitobject = other.gameObject.tag;
		
		if(hitobject == "ObstacleBall")
		{
			// Set the direction
			Vector3 dir = other.gameObject.transform.position - this.gameObject.transform.position;
			
			// Normalize the direction
			dir.Normalize();
			
			// Make the ball move in the oposite direction
			other.gameObject.gameObject.transform.rigidbody.velocity = dir * other.gameObject.rigidbody.velocity.magnitude;
		}
		
	}
}
