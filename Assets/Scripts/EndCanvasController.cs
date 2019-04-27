using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvasController : MonoBehaviour
{
    [SerializeField]
    Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 9; i++)
        {
            int score = HighScoreLogger.LookupScore(i);
            string scoretext = score == -1 ? "None" : score.ToString();
            scoreDisplay.text += "Hole " + i + ": " + scoretext + "\r\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void backToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
