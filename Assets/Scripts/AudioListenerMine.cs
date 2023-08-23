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
    [SerializeField] CinemachineBrain solocam;
    [SerializeField] GameObject w1;
    [SerializeField] GameObject w2;
    void Start()
    {
        //cam3d.LookAt = w1.transform;
        //cam3d.Follow = w2.transform;
        CinemachineBrain.SoloCamera = cam3d;
        view = GetComponent<PhotonView>();
        audioListener = GetComponent<AudioListener>();

        if (!view.IsMine)
        {
            audioListener.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
