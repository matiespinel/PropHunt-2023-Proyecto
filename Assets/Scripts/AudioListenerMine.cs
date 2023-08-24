using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class AudioListenerMine : MonoBehaviour
{
    private AudioListener audioListener;
    PhotonView view;
    [SerializeField] CinemachineFreeLook cam3d;
    void Start()
    {
        view = GetComponent<PhotonView>();
        audioListener = GetComponent<AudioListener>();

        if (!view.IsMine)
        {
            audioListener.gameObject.SetActive(false);
            cam3d.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
