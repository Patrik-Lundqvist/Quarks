﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles game operations
/// </summary>
public class GameManager : MonoBehaviour {

	// Is the game is running
	public bool isRunning = false;

	// Has a game ended
	public bool isGameOver = false;

	// Final time
	public float finalTime;

	// Current game message
	public string gameInfo;

	// Time between each obstacleball spawn
	private float nextSpawnTime = 5;

	// Timer for the game time
	private Timer gameTimer;

	public string highScore;

	// List of all the current obstacle balls
	private List<GameObject> ObstacleBallList = new List<GameObject>();

	// Music track
	OTSound music;

	// Music volume
	float musicVolume = 0.7f;

	// Singleton
	public GameManager gameManager;
	public static GameManager Instance { get; private set; }

	/// <summary>
	/// Gets the number of obstacle balls.
	/// </summary>
	/// <value>The number of obstacle balls.</value>
	public int NumberOfObstacleBalls
	{
		get { return ObstacleBallList.Count; }
	}

	/// <summary>
	/// Gets the current game time.
	/// </summary>
	/// <value>The current game time.</value>
	public float CurrentGameTime
	{
		get { return gameTimer.CurrentTime; }
	}

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {

		music = new OTSound("Music").Idle();

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
	public void BeginNewGame()
	{
		isGameOver = false;

		music.Loop().Play().Volume(musicVolume);

		ResetGame();

		// Lock the cursor
		Screen.lockCursor = true;

		// Hide the cursor
		Screen.showCursor = false;

		// Run the StartGame() function with a delay
		StartCoroutine(WaitAndStartGame());

	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	void StartGame()
	{
		// Spawn the first obstacle ball
		SpawnObstacleBall();

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
		// Set the game as over
		isGameOver = true;

		// Set the game as not running
		isRunning = false;

		// Stop the music
		StartCoroutine(FadeOutMusic());

		// Stop the timer
		gameTimer.StopTimer();

		// Set the final time
		finalTime = gameTimer.CurrentTime;

		// Set the game info to the current game time
		gameInfo = "Game Over";

		highScore = "New high score!";

		// Get the bottom wall
		GameObject bottomWall = GameObject.FindGameObjectWithTag("BottomWall");

		// Disable the bottom walls collider so that balls can drop <( -_- <)
		bottomWall.GetComponent<Collider>().enabled = false;

		// Set the global gravity with a downward force
		Physics.gravity = new Vector3(0, -300F, 0);

		// Show the cursor
		Screen.showCursor = true;

		// Unlock the cursor
		Screen.lockCursor = false;

		ShowScore();
	}


	public void ShowScore()
	{
		StartCoroutine(ShowScoreDelay());
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
	/// Fades the out music.
	/// </summary>
	/// <returns>The out music.</returns>
	public IEnumerator FadeOutMusic(){

			float t = musicVolume;

			while (t > 0.05f) 
			{

				t -= Time.deltaTime;
				if(isGameOver)
				{
					music.Volume(t);
				}
				

				yield return new WaitForSeconds(0.05f);
			}
			
			if(isGameOver)
			{
				music.Stop();
			}
	}

	public IEnumerator ShowScoreDelay(){
		

		// Wait for a second
		yield return new WaitForSeconds(2);


		new OTSound("BassDrum");

		GUIManager.Instance.showTimeTitle = true;


		yield return new WaitForSeconds(1);

		new OTSound("SnareDrum");

		GUIManager.Instance.showScore = true;

		yield return new WaitForSeconds(1);

		GUIManager.Instance.showHighScore = true;

		new OTSound("HighHat");
		new OTSound("FanFare");

	}



	/// <summary>
	/// Resets the game.
	/// </summary>
	void ResetGame()
	{
		GUIManager.Instance.showHighScore = false;
		GUIManager.Instance.showScore = false;
		GUIManager.Instance.showTimeTitle = false;

		// Reset players current power
		PlayerManager.Instance.powerCurrent = 0;
		
		// Reset spawn time
		nextSpawnTime = 5;

		// Reset timer
		gameTimer.Reset();

		// Get the bottom wall
		GameObject bottomWall = GameObject.FindGameObjectWithTag("BottomWall");
		
		// Enable the bottom wall
		bottomWall.GetComponent<Collider>().enabled = true;
		
		// Set the global gravity with a downward force
		Physics.gravity = new Vector3(0, 0, 0);

		RemoveObstacleBalls();

		// Spawn the player ball
		gameObject.GetComponent<NetworkManager>().SpawnMainPlayerBall();

	}

	void RemoveObstacleBalls()
	{
		foreach(GameObject ball in ObstacleBallList)
		{
			Destroy(ball);
		}
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
