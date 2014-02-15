using UnityEngine;
using System.Collections;

/// <summary>
/// A spell that pushes enemies away from the player ball
/// </summary>
public class Push : Spell {

	/// <summary>
	/// Constructor
	/// </summary>
	public Push()
	{
		// Set the powercost of the spell
		powerCost = 10.0f;
		// Set the sound clip
		soundClip = "Push";
	}

	/// <summary>
	/// Find all the surrounding targets
	/// </summary>
	public override void Precast()
	{
		FindTargets(200f);

		base.Precast();
	}

	/// <summary>
	/// Move all the surround targets
	/// </summary>
	public override void Casting()
	{
		// For every target
		foreach(GameObject ball in targets)
		{
			// Set the direction
			Vector3 dir = ball.transform.position - caster.transform.position;

			// Normalize the direction
			dir.Normalize();

			// Make the ball move in the oposite direction with a slightly slower speed
			ball.gameObject.transform.rigidbody.velocity=dir * ball.rigidbody.velocity.magnitude * 0.8f;

		}

		base.Casting();

	}
}
