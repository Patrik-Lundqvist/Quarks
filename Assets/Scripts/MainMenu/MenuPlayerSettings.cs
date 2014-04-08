using UnityEngine;
using System.Collections;

public class MenuPlayerSettings : MonoBehaviour {

    // The GUI skin for the menu
    public GUISkin MainMenuSkin;

    public delegate void OnStartClickEvent(MenuPlayerSettings menuPlayerSettings);
    public event OnStartClickEvent OnStartClick;

    // Mouse sense slider value
    public float MouseSenseValue;

    public string PlayerName = PlayerPrefs.GetString("playerName");


    void Start()
    {
        // Set the starting sense to 1 (read this value of disk in the future)
        MouseSenseValue = 1f;
    }

    /// <summary>
    ///     On every updated gui frame
    /// </summary>
    private void OnGUI()
    {
        // Set the GUI skin
        GUI.skin = MainMenuSkin;


        // Draw menu box
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 280, 0, 560, Screen.height), MainMenuSkin.GetStyle("box"));

        GUILayout.Label("Quarks", "lblTitle", GUILayout.Width(560));
            GUILayout.BeginArea(new Rect(560 / 2 - 167, 250, 334, 100));

                GUILayout.BeginHorizontal();

                    GUILayout.Label("Name:", "lblName", GUILayout.Width(80));
                    PlayerName = GUILayout.TextField(PlayerName, 15, GUILayout.Width(250), GUILayout.Height(30));

                GUILayout.EndHorizontal();

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(560 / 2 - 105, 330, 210, 100));
                GUILayout.BeginHorizontal();
                GUILayout.Label("Sensitivity:", "lblName", GUILayout.Width(160));
                    GUILayout.Label(MouseSenseValue.ToString("0.00"), GUILayout.Width(100));
                GUILayout.EndHorizontal();

                // Get the slider value
                MouseSenseValue = GUILayout.HorizontalSlider(MouseSenseValue, 0.0F, 2.0F, GUILayout.Width(200), GUILayout.Height(30));

            GUILayout.EndArea();


            GUILayout.FlexibleSpace();
            // Logic for the singleplayer button
            if (GUILayout.Button("Start", GUILayout.Width(560), GUILayout.Height(80)))
            {
                OnStartClick(this);
            }
            GUILayout.FlexibleSpace();
        GUILayout.EndArea();
	}
}
