using System;
using System.Collections.Generic;
using System.Globalization;
using HighscoreAPI;
using UnityEngine;
using HighscoreAPI.Models;
using Random = UnityEngine.Random;

/// <summary>
///     Draws and handles the logic for the main menu
/// </summary>
public class MenuMain : MonoBehaviour
{
    // The GUI skin for the menu
    public GUISkin MainMenuSkin;


    public Texture2D LoadingTexture = null;

    public delegate void OnSinglePlayerClickEvent();
    public event OnSinglePlayerClickEvent OnSinglePlayerClick;

    public delegate void OnAboutClickEvent();
    public event OnAboutClickEvent OnAboutClick;

    private const int ButtonHeight = 80;

    private bool _requestLoad;

    private bool _requestSuccess;


    private List<Score> _highscoreList = new List<Score>();

    /// <summary>
    ///     Start the instance.
    /// </summary>
    private void Start()
    {

        HighscoreAPIManager.Instance.Client.GetHighscores(5, ResultsCallBack);      
        _requestLoad = true;
    }

    /// <summary>
    ///     On every updated gui frame
    /// </summary>
    private void OnGUI()
    {
        // Set the GUI skin
        GUI.skin = MainMenuSkin;


        // Draw menu box
        GUILayout.BeginArea(new Rect(Screen.width/2 - 280, 0, 560, Screen.height), MainMenuSkin.GetStyle("box"));

        GUILayout.Label("Quarks", "lblTitle", GUILayout.Width(560));

        // Logic for the singleplayer button
        if (GUILayout.Button("Marathon", GUILayout.Width(560), GUILayout.Height(ButtonHeight)))
        {
            OnSinglePlayerClick();
        }

        // Logic for the multiplayer button ( not implemented yet )
        if (GUILayout.Button("About", GUILayout.Width(560), GUILayout.Height(ButtonHeight)))
        {
            OnAboutClick();
        }

        // Logic for the exit button
        if (GUILayout.Button("Exit", GUILayout.Width(560), GUILayout.Height(ButtonHeight)))
        {
            Application.Quit();
        }


        GUILayout.BeginArea(new Rect(560/2 - 167, Screen.height - 225, 334, 214), MainMenuSkin.GetStyle("boxHighscore"));
        if (_requestLoad)
        {
            GUILayout.BeginArea(new Rect(167, 107, 50, 50));

            var matrixBackup = GUI.matrix;
            float thisAngle = Time.frameCount*2;
            var pos = new Vector2(0, 0);
            GUIUtility.RotateAroundPivot(thisAngle, pos);
            var thisRect = new Rect(-25, -25, 50, 50);
            GUI.DrawTexture(thisRect, LoadingTexture);
            GUI.matrix = matrixBackup;

            GUILayout.EndArea();
        }
        else
        {
            if (!_requestSuccess)
            {
                GUILayout.Label("No connection","lblConnInfo", GUILayout.Width(284));
            }
            else
            {
                foreach (var highscore in _highscoreList)
                {
                    GUILayout.BeginHorizontal();
                        GUILayout.Label(highscore.Name, GUILayout.Width(180));
                        GUILayout.Label(highscore.ScorePoints.ToString(CultureInfo.InvariantCulture), "lblScore", GUILayout.Width(100));
                    GUILayout.EndHorizontal();
                }
            }

        }

        GUILayout.EndArea();


        GUILayout.EndArea();



    }

    private void ResultsCallBack(Response<List<Score>> response)
    {
        _requestSuccess = response.isSuccess;
        _requestLoad = false;
        _highscoreList = response.DataObject;
    }


}
