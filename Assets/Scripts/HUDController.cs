using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private int _score;
    [SerializeField]
    private Text scoreLabel;
    [SerializeField]
    private GameObject powerMeter;

    [SerializeField]
    private Image border;
    [SerializeField]
    private RawImage camera;

    private bool cameraEnabled = true;
    private bool canStroke = true;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        updateScore();
        setPower(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleRearCamera();

            //Debug.Log(enabled);
        }
    }

    void toggleRearCamera()
    {
        cameraEnabled = !cameraEnabled;
        if (cameraEnabled)
        {
            border.enabled = true;
            camera.enabled = true;
        }
        else
        {
            border.enabled = false;
            camera.enabled = false;
        }
    }

    public void endHole()
    {
        canStroke = false;
    }
    public void addScore(int toAdd)
    {
        if (canStroke)
        {
            _score += toAdd;
            updateScore();
        }    
    }

    void updateScore()
    {
        scoreLabel.text = "Strokes: " + _score.ToString();
    }

    public void setPower(float power)
    {
        powerMeter.transform.localScale = new Vector3(1.0f, power, 1.0f);
    }
}
