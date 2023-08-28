using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class AudioListenerMine : MonoBehaviour
{
    #region vars
    private AudioListener audioListener;
    private PhotonView view;
    [SerializeField] private CinemachineFreeLook cam3d;
    #endregion
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
}
