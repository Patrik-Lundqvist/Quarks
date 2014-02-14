using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {

	private GameObject scripts;

	public Spell firstSpell;

	public void CastSpell()
	{
		GameObject playerBall = PlayerManager.Instance.playerBall;
		if(playerBall == null)
		{
			return;
		}
		   
		firstSpell = scripts.AddComponent<Push>();
		firstSpell.Setup(playerBall);
		firstSpell.Cast();
	}

	// Use this for initialization
	void Start () {
		scripts = GameObject.Find("_SCRIPTS");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Q))
			CastSpell();
	}
}
