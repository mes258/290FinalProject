using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvasController : MonoBehaviour
{
    [SerializeField]
    Text scoreDisplay;

    int[] pars = {3, 4, 5, 6, 5, 8, 14, 0, 60};
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay.text += "Hole \tPar \t \tScore \r\n";
        for (int i = 1; i <= 9; i++)
        {
            int score = HighScoreLogger.LookupScore(i);
            string scoretext = score == -1 ? "None" : score.ToString();
            //scoreDisplay.text += "Hole " + i + ": \t " + scoretext + "\r\n";
            scoreDisplay.text += i + "\t\t\t" + pars[i-1] + "\t\t\t" + scoretext + "\r\n";
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
