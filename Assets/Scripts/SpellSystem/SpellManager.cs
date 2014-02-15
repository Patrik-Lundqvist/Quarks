using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all the available spells and fires them on keystrokes.
/// </summary>
public class SpellManager : MonoBehaviour {
	
	// Get the script object in the scene
	private GameObject scripts;

	// Spell to cast
	public Spell castSpell;

	/// <summary>
	/// Try to cast a spell.
	/// </summary>
	/// <param name="spellNumber">Spell number.</param>
	public void CastSpell(Spell spell)
	{
		// Get the casters player ball
		GameObject playerBall = PlayerManager.Instance.playerBall;

		// If we have no player ball, do nothing
		if(playerBall == null)
		{
			return;
		}

		// Setup the spell
		spell.Setup(playerBall);
		// Cast the spell
		spell.Cast();
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start () {
		// Get the scene script object
		scripts = GameObject.Find("_SCRIPTS");
	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		// Check key input
		if (Input.GetKeyUp(KeyCode.Q))
			CastSpell(scripts.AddComponent<Push>());
	}
}
