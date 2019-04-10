using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject rearViewCamera;

    [SerializeField]
    private GameObject rearViewCameraBorder;

    [SerializeField]
    private GameObject levelEndGameObj;
    private Canvas LevelEndCanvas;

    [SerializeField]
    private GameObject SettingsGameObj;
    private Canvas SettingsCanvas;

    private Image border;
    private RawImage camera;

    private bool cameraEnabled = true;
    private bool settingsEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        border = rearViewCameraBorder.GetComponent<Image>();
        camera = rearViewCamera.GetComponent<RawImage>();

        SettingsCanvas = SettingsGameObj.GetComponent<Canvas>();
        SettingsCanvas.enabled = false;

        LevelEndCanvas = levelEndGameObj.GetComponent<Canvas>();
        LevelEndCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleRearCamera();

            //Debug.Log(enabled);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleSettings();
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

    void toggleSettings()
    {
        settingsEnabled = !settingsEnabled;
        if(settingsEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SettingsCanvas.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SettingsCanvas.enabled = false;
        }
    }

    public void showLevelEnd()
    {
        LevelEndCanvas.enabled = true;
    }
}
