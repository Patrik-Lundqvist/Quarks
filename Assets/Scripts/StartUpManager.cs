using HighscoreAPI;
using UnityEngine;
using HighscoreAPI.Models;

public class StartUpManager : MonoBehaviour
{

    // The GUI skin for the menu
    public GUISkin MainMenuSkin;
    public Texture2D LoadingTexture = null;

    private string _updateInfo;
    private bool _loadLevel;

    /// <summary>
    ///     Start the instance.
    /// </summary>
    private void Start()
    {
        _updateInfo = string.Empty;
        HighscoreAPIManager.Instance.Client.GetVersion(GetVersionCallback);
    }

    private void GetVersionCallback(Response<GameVersion> response)
    {
        if (response.isSuccess)
        {
            if (response.DataObject.UpdateExists)
            {
                _updateInfo = "New version is available, please update";
            }

            if (response.DataObject.UpdateRequired)
            {
                _updateInfo = "New version is required to save score, please update";
            }

            if (!response.DataObject.UpdateRequired && !response.DataObject.UpdateExists)
            {
                _loadLevel = true;
            }
        }
        else
        {
            _updateInfo = "No connection, might not be able to save score";
        }

    }


    private void LoadMainMenu()
    {
        Application.LoadLevel("mainMenu");
    }

    private void Update()
    {
        if (_loadLevel)
        {
            LoadMainMenu();
        }
    }

    /// <summary>
    ///     On every updated gui frame
    /// </summary>
    private void OnGUI()
    {
        // Set the GUI skin
        GUI.skin = MainMenuSkin;


        // Draw menu box
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 175, 800, 350), MainMenuSkin.GetStyle("box"));

        if (string.IsNullOrEmpty(_updateInfo))
        {
            GUILayout.Label("Connecting", "lblConnecting", GUILayout.Width(800));
            GUILayout.BeginArea(new Rect(400, 160, 50, 50));

                var matrixBackup = GUI.matrix;
                float thisAngle = Time.frameCount * 2;
                var pos = new Vector2(0, 0);
                GUIUtility.RotateAroundPivot(thisAngle, pos);
                var thisRect = new Rect(-25, -25, 50, 50);
                GUI.DrawTexture(thisRect, LoadingTexture);
                GUI.matrix = matrixBackup;

            GUILayout.EndArea();
        }
        else
        {
            GUILayout.Label(_updateInfo, "lblVersionInfo", GUILayout.Width(800));

            if (GUILayout.Button("Play anyway", GUILayout.Width(800), GUILayout.Height(80)))
            {
                LoadMainMenu();
            }
            if (GUILayout.Button("Exit", GUILayout.Width(800), GUILayout.Height(80)))
            {
                Application.Quit();
            }
        }


        GUILayout.EndArea();


    }

}
