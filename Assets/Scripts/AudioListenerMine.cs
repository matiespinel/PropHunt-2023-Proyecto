using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioListenerMine : MonoBehaviour
{
    private AudioListener audioListener;
    PhotonView view;
    void Start()
    {

        view = GetComponenet
        audioListener = GetComponent<AudioListener>();

        if (!photonView.IsMine)
        {
            audioListener.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
