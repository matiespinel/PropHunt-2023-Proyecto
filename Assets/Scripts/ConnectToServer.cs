using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_Text buttonText;
    public TMP_InputField userName;

    public void OnClickConnect()
    {
        if (userName.text.Length >= 1)
        {
            PhotonNetwork.NickName = userName.text;
            buttonText.text = "Conectando....";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
