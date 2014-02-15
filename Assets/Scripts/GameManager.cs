using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles game operations
/// </summary>
public class GameManager : MonoBehaviour {

	// Is the game is running
	public bool isRunning = false;

	// Current game message
	public string gameInfo;

	// Time between each obstacleball spawn
	private float nextSpawnTime = 5;

	// Timer for the game time
	private Timer gameTimer;

	// List of all the current obstacle balls
	private List<GameObject> ObstacleBallList = new List<GameObject>();

	// Singleton
	public GameManager gameManager;
	public static GameManager Instance { get; private set; }

	// Delegate for a delayed firing
	delegate void DelayedMethod();

	/// <summary>
	/// Gets the number of obstacle balls.
	/// </summary>
	/// <value>The number of obstacle balls.</value>
	public int NumberOfObstacleBalls
	{
		get { return ObstacleBallList.Count; }
	}

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		// Get and set the game timer
		gameTimer = gameObject.GetComponent<Timer>();

		// Let's begin a new game
		BeginNewGame();
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () 
	{
		if(isRunning){
			if(gameTimer.CurrentTime > nextSpawnTime)
			{
				SpawnObstacleBall();
				nextSpawnTime += 10;
			}

		}
	}

	/// <summary>
	/// Begins a new game.
	/// </summary>
	void BeginNewGame()
	{
		// Hide the cursor
		Screen.showCursor = false;

		// Spawn the player ball
		gameObject.GetComponent<NetworkManager>().SpawnMainPlayerBall();

		// Spawn the first obstacle ball
		SpawnObstacleBall();

		// Run the StartGame() function with a delay
		StartCoroutine(WaitAndStartGame());
	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	void StartGame()
	{
		// Start the game timer
		gameTimer.StartTimer();

		// Set the game as running
		isRunning = true;
	}

	/// <summary>
	/// Function fired when the player loses.
	/// </summary>
	public void GameOver()
	{
		// Stop the timer
		gameTimer.StopTimer();

		// Set the game as not running
		isRunning = false;

		// Set the game info to the current game time
		gameInfo = gameTimer.CurrentTimeStamp;

		// Get the bottom wall
		GameObject bottomWall = GameObject.FindGameObjectWithTag("BottomWall");

		// Disable the bottom walls collider so that balls can drop <( -_- <)
		bottomWall.GetComponent<Collider>().enabled = false;

		// Set the global gravity with a downward force
		Physics.gravity = new Vector3(0, -300F, 0);

		// Show the cursor
		Screen.showCursor = true;
	}

	/// <summary>
	/// Spawn an obstacle ball.
	/// </summary>
	void SpawnObstacleBall()
	{
		gameObject.GetComponent<NetworkManager>().SpawnObstacleBall();

	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// Save a reference as our singleton instance
		Instance = this;
	}

	/// <summary>
	/// Adds an obstacle ball to the list of balls.
	/// </summary>
	/// <param name="obstacleBall">Obstacle ball.</param>
	public void AddObstacleBall(GameObject obstacleBall){
		ObstacleBallList.Add(obstacleBall);
	}

	/// <summary>
	/// Deletes an obstacle ball from the list of balls
	/// </summary>
	/// <param name="obstacleBall">Obstacle ball.</param>
	public void DeleteObstacleBall(GameObject obstacleBall){
		ObstacleBallList.Remove(obstacleBall);
	}

	/// <summary>
	/// Waits for 3 seconds and starts the game
	/// </summary>
	/// <returns>The and do.</returns>
	/// <param name="time">Time.</param>
	/// <param name="method">Method.</param>
	IEnumerator WaitAndStartGame()
	{
		// Cound down from 3
		for (float i = 3; i > 0; i--)
		{
			// Set game info message to the current second
			gameInfo = i.ToString();

			// Wait for a second
			yield return new WaitForSeconds(1);
		}

		// Run method
		StartGame();
	}

}
