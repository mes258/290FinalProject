using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvasController : MonoBehaviour
{
    int[] pars = {3, 4, 5, 7, 5, 12, 14, 10, 40};
    int totalPar = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        var h = 0.3f; // proportional height (0..1)
        var windowX = (float)((Screen.width / 2) - 150f);
        var windowY = (float)(Screen.height * (1 - (h + 0.2))) / 2;
        var windowWidth = 300f;
        var windowHeight = (float)(Screen.height * h);

        Rect windowRect = new Rect(windowX, windowY, windowWidth, windowHeight);

        windowRect = GUILayout.Window(0, windowRect, AddScores, "<b>Score Card</b>");
    }

    // Make the contents of the window
    void AddScores(int windowID)
    {
        var centerAlignedLabel = new GUIStyle(GUI.skin.label);
        centerAlignedLabel.alignment = TextAnchor.MiddleCenter;
        //centerAlignedLabel.stretchWidth = true;
        centerAlignedLabel.fixedWidth = 100;
        centerAlignedLabel.fontSize = 20;

        GUILayout.BeginVertical(GUILayout.Width(300));

        GUILayout.BeginHorizontal();
        GUILayout.Label("<b>Hole</b>", centerAlignedLabel);
        GUILayout.Label("<b>Par</b>", centerAlignedLabel);
        GUILayout.Label("<b>Score</b>", centerAlignedLabel);
        GUILayout.EndHorizontal();
        int totalScore = 0;
        bool allHolesFinished = true;
        for (var i = 1; i < 10; i++)
        {
            int score = HighScoreLogger.LookupScore(i);
            totalScore += score;
            string scoretext = "";
            if (score == -1)
            {
                scoretext = "None";
                allHolesFinished = false;
            }
            else
            {
                scoretext = score.ToString();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label(i + "", centerAlignedLabel);
            GUILayout.Label(pars[i - 1] + "", centerAlignedLabel);
            GUILayout.Label(scoretext, centerAlignedLabel);
            GUILayout.EndHorizontal();
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("<b>Total:</b>", centerAlignedLabel);
        GUILayout.Label("<b>" + totalPar.ToString() + "</b>", centerAlignedLabel);
        if (allHolesFinished)
        {
            GUILayout.Label("<b>" + totalScore.ToString() + "</b>", centerAlignedLabel);
        }
        else
        {
            GUILayout.Label("<b>N/A</b>", centerAlignedLabel);

        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void backToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void resetScores()
    {
        PlayerPrefs.DeleteAll();
    }
}
