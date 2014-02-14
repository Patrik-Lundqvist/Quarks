using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float powerMax = 100;
	public float powerCurrent = 0;

	public GameObject playerBall;

	public PlayerManager playerManager;

	public static PlayerManager Instance { get; private set; }

	void Awake()
	{
		// Save a reference to the AudioHandler component as our singleton instance
		Instance = this;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPlayerBall(GameObject playerBall)
	{
		this.playerBall = playerBall;
	}

}
