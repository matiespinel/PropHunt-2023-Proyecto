using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class DeathHUDScript : MonoBehaviourPunCallbacks
{
    private void ActivateDeathHUD()
    {
        Entity.OnEntityDeath += EnableDeathHUD;
    }

    private void DeactivateDeathHUD()
    {
        Entity.OnEntityDeath += DisableDeathHUD;
    }
    private void EnableDeathHUD()
    {
        this.gameObject.SetActive(true);
    }

    private void DisableDeathHUD()
    {
        this.gameObject.SetActive(false);
    }

     public void QuitGame()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("Menu");
        Debug.Log("Disconnected");
    }
    void Start()
    {
        ActivateDeathHUD();
        DisableDeathHUD();
    }
}
