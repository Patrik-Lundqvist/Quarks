using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool Running = false;
	public string GameInfo;

	private float nextSpawnTime = 5;
	private Timer gameTimer;


	private List<GameObject> ObstacleBallList = new List<GameObject>();

	// Singleton
	public GameManager gameManager;
		
	public static GameManager Instance { get; private set; }

	delegate void DelayedMethod();

	public int NumberOfObstacleBalls
	{
		get { return ObstacleBallList.Count; }
	}

	// Use this for initialization
	void Start () {
		gameTimer = gameObject.GetComponent<Timer>();
		BeginNewGame();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Running){
			if(gameTimer.CurrentTime > nextSpawnTime)
			{
				SpawnObstacleBall();
				nextSpawnTime += 10;
			}

		}
	}

	void BeginNewGame()
	{
		gameObject.GetComponent<NetworkManager>().SpawnMainPlayerBall();

		SpawnObstacleBall();

		StartCoroutine(WaitAndDo(5, StartGame));
	}

	void StartGame()
	{
		gameObject.GetComponent<Timer>().StartTimer();

		Running = true;
	}

	public void GameOver()
	{
		Running = false;
		GameInfo = gameObject.GetComponent<Timer>().CurrentTimeStamp;
		Screen.showCursor = true;
		GameObject bottomWall = GameObject.FindGameObjectWithTag("BottomWall");
		bottomWall.GetComponent<Collider>().enabled = false;
		Physics.gravity = new Vector3(0, -20.0F, 0);
	}


	void SpawnObstacleBall()
	{
		gameObject.GetComponent<NetworkManager>().SpawnObstacleBall();

	}

	void Awake()
	{
		// Save a reference to the AudioHandler component as our singleton instance
		Instance = this;
	}

	public void AddObstacleBall(GameObject obstacleBall){
		ObstacleBallList.Add(obstacleBall);
	}
	public void DeleteObstacleBall(GameObject obstacleBall){
		ObstacleBallList.Remove(obstacleBall);
	}

	IEnumerator WaitAndDo(float time, DelayedMethod method)
	{

		for (float i = 3; i > 0; i--)
		{
			GameInfo = i.ToString();
			yield return new WaitForSeconds(1);
		}

		method();
	}

}
