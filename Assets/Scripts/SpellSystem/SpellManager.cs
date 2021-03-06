﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Contains all the available spells and fires them on keystrokes.
/// </summary>
public class SpellManager : MonoBehaviour {

	public List<GameObject> SpellPrefabList = new List<GameObject>();

	// Spell to cast
	public Spell castSpell;

	// Singleton
	public SpellManager spellManager;
	public static SpellManager Instance { get; private set; }

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Start () {

	
	}
	
	public List<Spell> GetSpells()
	{
		var spellList = new List<Spell>();

		foreach(var spellPrefab in SpellPrefabList)
		{
			spellList.Add(spellPrefab.gameObject.GetComponent<Spell>());
		}

		return spellList;
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// Save a reference to our singleton instance
		Instance = this;
	}

	/// <summary>
	/// Try to cast a spell.
	/// </summary>
	/// <param name="spellNumber">Spell number.</param>
	public void CastSpell(GameObject spell)
	{


		// If we have no player ball, do nothing
		if(PlayerManager.Instance.playerBall == null)
		{
			return;
		}

		// Get the casters player ball
		var playerBallScript = PlayerManager.Instance.playerBall.GetComponent<PlayerBall>();
		
		var spellToCast = spell.gameObject.GetComponent<Spell>();

		// Check if we have enough power for the spell
		if(PlayerManager.Instance.powerCurrent < spellToCast.powerCost)
		{
			GUIManager.Instance.NoPowerNotice(spellToCast.powerCost);
			return;
		}
		
		// Remove the the power for the player
		PlayerManager.Instance.powerCurrent -= spellToCast.powerCost;

		var SpellInstance = (Instantiate(spell, playerBallScript.gameObject.transform.position, Quaternion.identity) as GameObject).GetComponent<Spell>();

		// Setup the spell
		SpellInstance.Setup(playerBallScript);

		if(SpellInstance.isSelfCast)
			SpellInstance.gameObject.transform.parent = SpellInstance.caster.gameObject.transform;

		// Cast the spell
		SpellInstance.Cast();


	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		// Check key input
		if (Input.GetKeyUp(KeyCode.Q))
		{
			if(SpellPrefabList.ElementAtOrDefault(0) != null)
				CastSpell(SpellPrefabList[0]);
		}

		// Check key input
		if (Input.GetKeyUp(KeyCode.W))
		{
			if(SpellPrefabList.ElementAtOrDefault(1) != null)
				CastSpell(SpellPrefabList[1]);
		}
		// Check key input
		if (Input.GetKeyUp(KeyCode.E))
		{
			if(SpellPrefabList.ElementAtOrDefault(2) != null)
				CastSpell(SpellPrefabList[2]);
		}

		// Check key input
		if (Input.GetKeyUp(KeyCode.R))
		{
			if(SpellPrefabList.ElementAtOrDefault(3) != null)
				CastSpell(SpellPrefabList[3]);
		}
	}

}
