using UnityEngine;
using System.Collections;

/// <summary>
/// A spell that pushes enemies away from the player ball
/// </summary>
public class Freeze : Spell {

	Vector2 startSize;
	Vector2 endSize;
	
	float spellSize;
	float moveTime = 0.0f;
	
	OTSprite sprite;     

	/// <summary>
	/// Constructor
	/// </summary>
    public Freeze()
	{
		// Set the powercost of the spell
		powerCost = 1.0f;
		// Set the sound clip
		soundClip = "Push";
		// Set the name of the spell
		spellName = "Freeze";

		isSelfCast = false;
	}

	/// <summary>
	/// Find all the surrounding targets
	/// </summary>
	public override void Precast()
	{
		spellSize = 320;

		// get this star's sprite class
		sprite = GetComponent<OTSprite>();

		sprite.size = new Vector2(0,0);
		startSize = sprite.size;
        sprite.position = new Vector2(caster.transform.position.x, caster.transform.position.y);
		endSize = new Vector2(spellSize, spellSize);


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
		var hitobject = other.gameObject.tag;
		
		if(hitobject == "ObstacleBall")
		{
		    Ball obstacleball = other.GetComponent<ObstacleBall>();

            obstacleball.Freeze(3f);
		}
		
	}
}
