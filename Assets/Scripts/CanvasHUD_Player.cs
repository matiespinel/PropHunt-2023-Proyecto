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
    public TMP_Text PropCountText;
    public GameManager gameManager;
    private PhotonView PV;
    public Canvas canvasHUD;
    public GameObject QuitPanel;
    #endregion
    public void SetPlayerInfo(Player _player)
     {
         playerName.text = _player.NickName;
     }
     public void LeaveRoom()
     {
         PhotonNetwork.LeaveRoom();
     }
    void Start()
    {
        gameManager = GameObject.Find("[GAMEMANAGER]").GetComponent<GameManager>();
       PV = GetComponent<PhotonView>();
       PropCountText.text = "Props: " + gameManager.propCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            canvasHUD.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitPanel.SetActive(true);
            }
            playerName.text = PhotonNetwork.NickName;
        }
    }
}
