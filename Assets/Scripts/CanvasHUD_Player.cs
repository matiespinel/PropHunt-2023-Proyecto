using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;

public class CanvasHUD_Player : MonoBehaviour
{
    #region vars
    public TMP_Text playerName;
    private PhotonView PV;
    public Canvas canvasHUD;
    #endregion
    public void SetPlayerInfo(Player _player)
     {
         playerName.text = _player.NickName;
     }
    void Start()
    {
       PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            canvasHUD.gameObject.SetActive(true);
            playerName.text = PhotonNetwork.NickName;
        }
    }
}
