using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class AudioListenerMine : MonoBehaviour
{
    private AudioListener audioListener;
    PhotonView view;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        view = GetComponent<PhotonView>();
        audioListener = GetComponent<AudioListener>();

        if (!view.IsMine)
        {
            cam.gameObject.SetActive(false);
            audioListener.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
