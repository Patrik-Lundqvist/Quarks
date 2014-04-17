using UnityEngine;
using System.Collections;

public class MenuAbout : MonoBehaviour {

    // The GUI skin for the menu
    public GUISkin MainMenuSkin;

    public delegate void OnBackClickEvent();
    public event OnBackClickEvent OnBackClick;


    /// <summary>
    /// On every updated gui frame
    /// </summary>
    private void OnGUI()
    {
        // Set the GUI skin
        GUI.skin = MainMenuSkin;


        // Draw menu box
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 280, 0, 560, Screen.height), MainMenuSkin.GetStyle("box"));

        GUILayout.Label("About", "lblTitle", GUILayout.Width(560));
            GUILayout.BeginArea(new Rect(560 / 2 - 200, 250, 400, 100));


                    GUILayout.Label("Game idea from Particles", "lblName", GUILayout.Width(400));


                    GUILayout.Label("Music by Dimrain47", "lblName", GUILayout.Width(400));

            GUILayout.EndArea();


            GUILayout.FlexibleSpace();
            // Logic for the singleplayer button
            if (GUILayout.Button("Back", GUILayout.Width(560), GUILayout.Height(80)))
            {
                OnBackClick();
            }
            GUILayout.FlexibleSpace();
        GUILayout.EndArea();
	}
}
