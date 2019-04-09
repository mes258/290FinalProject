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


    private Image border;
    private RawImage camera;

    private bool enabled = true;
    // Start is called before the first frame update
    void Start()
    {
        border = rearViewCameraBorder.GetComponent<Image>();
        camera = rearViewCamera.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            enabled = !enabled;
            if(enabled)
            {
                border.enabled = true;
                camera.enabled = true;
            }
            else
            {
                border.enabled = false;
                camera.enabled = false;
            }

            //Debug.Log(enabled);
        }
    }
}
