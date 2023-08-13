using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    [SerializeField] Camera FirstCam;
    [SerializeField] Camera ThirdCam;
    [SerializeField] KeyCode SwitchKey;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(SwitchKey))
        {
            CamSwitch(ThirdCam, FirstCam);
        }
    }

    private void CamSwitch(Camera cam1, Camera cam2)
    {
        /* cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true); */
        cam1.transform.position = cam2.transform.position;
    }
}
