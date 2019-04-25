using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreLogger : MonoBehaviour
{
    public int currentlevel;
    string currentKey()
    {
        return makeKey(currentlevel);
    }

    static string makeKey(int levelnum)
    {
        return "Level" + levelnum + "High";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewScore(int score)
    {

        if (!PlayerPrefs.HasKey(currentKey()) || PlayerPrefs.GetInt(currentKey()) > score)
        {
            PlayerPrefs.SetInt(currentKey(), score);
        }
    }

    public int lookupScore()
    {
        if(!PlayerPrefs.HasKey(currentKey()))
        {
            return -1;
        }
        else
        {
            return PlayerPrefs.GetInt(currentKey());
        }
    }

    public static int LookupScore(int levelNum)
    {
        string key = makeKey(levelNum);
        if (!PlayerPrefs.HasKey(key))
        {
            return -1;
        }
        else
        {
            return PlayerPrefs.GetInt(key);
        }
    }
}
