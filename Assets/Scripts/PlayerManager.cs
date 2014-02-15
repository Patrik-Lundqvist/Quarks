using UnityEngine;
using System.Collections;

/// <summary>
/// Class containing data on the current player
/// </summary>
public class PlayerManager : MonoBehaviour {

	// The max power value
	public float powerMax = 100;

	// The current power value
	public float powerCurrent = 0;

	// Stores a reference to the main player ball
	public GameObject playerBall;

	// Singleton
	public PlayerManager playerManager;
	public static PlayerManager Instance { get; private set; }

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// Save a reference to our singleton instance
		Instance = this;
	}

	/// <summary>
	/// Sets the main player ball.
	/// </summary>
	/// <param name="playerBall">Player ball.</param>
	public void SetPlayerBall(GameObject playerBall)
	{
		this.playerBall = playerBall;
	}

	/// <summary>
	/// Delete the main player ball.
	/// </summary>
	public void DeletePlayerBall()
	{
		this.playerBall = null;
	}

}
