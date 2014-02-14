using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUISkin GameUISkin;
	
	private GameManager gameManager;
	private PlayerManager playerManager;


	// PROGRESS BAR
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	public Texture GUI_BG;

	public Vector2 pos = new Vector2(0,0);
	private Vector2 size = new Vector2(600,18);
	
	public Texture2D emptyTex;
	public Texture2D fullTex;

	void Start()
	{
		gameManager = gameObject.GetComponent<GameManager>() as GameManager;
	}

	void OnGUI()
	{
		// Load skin
		GUI.skin = GameUISkin;

		//Current Time
		GUI.Label(new Rect(10,10,100,45), gameManager.NumberOfObstacleBalls.ToString());

		// BOTTOM UI GROUP
		GUI.BeginGroup(new Rect(0, Screen.height - 108, Screen.width, 108 ));
			
		// Add box at bottom
		GUI.DrawTexture (new Rect (-1,0,1282,108),GUI_BG);
		
		if(GameManager.Instance.Running)
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

			GUI.Label(new Rect(665,2,50,45), PlayerManager.Instance.powerCurrent.ToString("0") + "/" + PlayerManager.Instance.powerMax, GameUISkin.GetStyle("UIBottomLabelPower"));

			GUI.EndGroup();
		}
		else
		{
			// Power-label
			GUI.Label(new Rect( Screen.width / 2 - 50, 30, 100, 50), GameManager.Instance.GameInfo, GameUISkin.GetStyle("UIGameInfo"));

		}
				

		GUI.EndGroup();


	}
}
