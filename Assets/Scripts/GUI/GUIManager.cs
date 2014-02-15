using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class for the user interface.
/// </summary>
public class GUIManager : MonoBehaviour {

	// The UI skin
	public GUISkin GameUISkin;

	// Singleton
	public GUIManager guiManager;
	public static GUIManager Instance { get; private set; }

	// The main player ball
	private GameObject playerBall;

	// Game object to spawn as a notice
	public GameObject playerNotice;

	// PROGRESS BAR
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	public Texture GUI_BG;

	public Vector2 pos = new Vector2(0,0);
	private Vector2 size = new Vector2(600,18);
	
	public Texture2D emptyTex;
	public Texture2D fullTex;
	// END OF PROGRESS BAR

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		// Save a reference to our singleton instance
		Instance = this;
	}

	/// <summary>
	/// Show a notice and play a sound
	/// </summary>
	/// <param name="power">Power.</param>
	public void NoPowerNotice(float power)
	{
		// Play sound
		new OTSound("NoPower");

		// Show notification
		ShowPlayerNotice("Need " + power + " power");

	}

	// Shows a message over the main player ball
	public void ShowPlayerNotice(string notice)
	{
		GameObject tempGO = Instantiate(playerNotice, Camera.main.WorldToViewportPoint(PlayerManager.Instance.playerBall.transform.position), Quaternion.identity) as GameObject;
		tempGO.GetComponent<GUIText>().text = notice;
	}

	/// <summary>
	/// Updates the GUI
	/// </summary>
	void OnGUI()
	{
		// Load skin
		GUI.skin = GameUISkin;

		//Current Time
		GUI.Label(new Rect(10,10,100,45), GameManager.Instance.NumberOfObstacleBalls.ToString());

		// BOTTOM UI GROUP
		GUI.BeginGroup(new Rect(0, Screen.height - 108, Screen.width, 108 ));
			
		// Add box at bottom
		GUI.DrawTexture (new Rect (-1,0,1282,108),GUI_BG);
		
		if(GameManager.Instance.isRunning)
		{
			// Draw power box
			GUI.BeginGroup(new Rect(68,26,750,50));

				// Power-label
				GUI.Label(new Rect(0,0,50,45), "Power:", GameUISkin.GetStyle("UIBottomLabel"));
				
				// Draw power bar
				GUI.BeginGroup(new Rect(110,3, size.x, size.y));	

					// Draw bar based on player power
					GUI.BeginGroup(new Rect(0, 0, size.x * PlayerManager.Instance.powerCurrent / PlayerManager.Instance.powerMax, size.y));
					
						GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);
					
					GUI.EndGroup();
					
					// Draw border
					GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex, progress_empty);
				
				GUI.EndGroup();

			GUI.Label(new Rect(665,2,50,45), Truncate(PlayerManager.Instance.powerCurrent, 0).ToString("0") + "/" + PlayerManager.Instance.powerMax, GameUISkin.GetStyle("UIBottomLabelPower"));

			GUI.EndGroup();
		}
		else
		{
			// Power-label
			GUI.Label(new Rect( Screen.width / 2 - 50, 30, 100, 50), GameManager.Instance.gameInfo, GameUISkin.GetStyle("UIGameInfo"));

		}

		GUI.EndGroup();
	}

	/// <summary>
	/// Truncate the specified value and digits. (i.e set the number of displayed decimals)
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="digits">Digits.</param>
	public float Truncate(float value, int digits)
	{
		double mult = Math.Pow(10.0, digits);
		double result = Math.Truncate( mult * value ) / mult;
		return (float) result;
	}
}
