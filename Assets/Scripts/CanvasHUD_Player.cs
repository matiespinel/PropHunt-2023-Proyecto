using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CanvasHUD_Player : MonoBehaviourPunCallbacks
{
    #region vars
    public TMP_Text playerName;
    public TMP_Text PropCountText;
    public GameManager gameManager;
    private PhotonView PV;
    public Canvas canvasHUD;
    public GameObject QuitPanel;
    public TMP_Text  InicioText;

    public TMP_Text tiempoo;
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
        if (PV.IsMine)
            playerName.text = PhotonNetwork.NickName;
        canvasHUD.gameObject.SetActive(true);
        StartCoroutine(StartGame());
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        PropCountText.text = "Props: " + RoleManager.propCount;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && QuitPanel.activeSelf == true)
        {
            QuitPanel.SetActive(false);
        }

        double time = gameManager.timer;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timerText = string.Format("{0:D2}:{1:D2}:{2:D3}", 
            timeSpan.Minutes, 
            timeSpan.Seconds, 
            timeSpan.Milliseconds);
        tiempoo.text = timerText;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(15f);
        InicioText.gameObject.SetActive(false);

    }
}