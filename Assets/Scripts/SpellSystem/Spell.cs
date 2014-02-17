using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Abstract class containing a frame for spells and some basic functionality
/// </summary>
public abstract class Spell : MonoBehaviour {

	// If the spell is casted on the player ball
	public bool isSelfCast;

	// Current status of the spell
	public SpellStatus status = SpellStatus.Ready;

	// Gameobject who casted the spell
	public Ball caster;

	// A list of targets found
	public List<GameObject> targets;

	// The power cost of the spell
	public float powerCost;

	// Name of the spell
	public string spellName;

	// A soundclip which is played when the spell is casted
	public string soundClip;

	// The spell icon shown in the UI
	public Texture txIcon;

	/// <summary>
	/// Setup for the spell.
	/// </summary>
	/// <param name="caster">Caster.</param>
	public virtual void Setup(Ball caster)
	{
		// Initialize the list
		targets = new List<GameObject>();

		// Set the caster
		this.caster = caster;
	}

	/// <summary>
	/// Cast the spell.
	/// </summary>
	public virtual void Cast()
	{
		Precast();
	}

	/// <summary>
	/// Function runs berfore the spell is casted
	/// </summary>
	public virtual void Precast()
	{
		// Check if we have a sound clip
		if(!string.IsNullOrEmpty(soundClip))
		{
			// Play the sound clip
			new OTSound(soundClip);
		}

		// Set status to casting
		status = SpellStatus.Casting;
	}

	/// <summary>
	/// Functions is called every frame while the state is "Casting"
	/// </summary>
	public virtual void  Casting()
	{
	
	}

	/// <summary>
	/// Sets the state to finished
	/// </summary>
	public virtual void EndCasting()
	{
		status = SpellStatus.Finished;
		CleanUp();
	}

	/// <summary>
	/// Cleans up after the spell.
	/// </summary>
	public virtual void  CleanUp()
	{
		// Delete this spell object
		Destroy(this.gameObject);
	}

	/// <summary>
	/// Find targets in a specified raduis.
	/// </summary>
	/// <param name="Radius">Radius.</param>
	public void FindTargets(float Radius)
	{
		// Clear the target list
		targets.Clear();

		// An array of colliders
		Collider[] Colliders;

		// Spawn a collider sphere with the specified radius
		Colliders = Physics.OverlapSphere(caster.gameObject.transform.position, Radius);

		// For every hit, add the target to the list
		foreach (Collider hit in Colliders) {

			// Check if the target is an obstacle ball
			if (hit && hit.tag == "ObstacleBall"){

				// Add to target list
				targets.Add(hit.gameObject);
			}
		}
	}



	/// <summary>
	/// Update is called every frame.
	/// </summary>
	void Update()
	{
		// If the state is set to casting, run the casting method
		if(status == SpellStatus.Casting)
		{
			Casting();
		}
	}
	

}
