using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;



public class RoleAssigner : MonoBehaviourPunCallbacks
{
    public int Roles = 0;
    public static RoleAssigner rol;
    void Awake()
    {
        if (RoleAssigner.rol == null)
        {
            RoleAssigner.rol = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

















    //public enum PlayerRole
    //{
    //    Cazador,
    //    Prop
    //}

    //private void Start()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        int randomPlayerIndex = Random.Range(0, PhotonNetwork.PlayerList.Length); // Generar un índice aleatorio
    //        photonView.RPC("AssignRoles", RpcTarget.All, randomPlayerIndex);
    //    }




    //}

    //[PunRPC]
    //private void AssignRoles(int hunterIndex)
    //{
    //    for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
    //    {
    //        Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];
    //        PlayerRole playerRole = (i == hunterIndex) ? PlayerRole.Cazador : PlayerRole.Prop;

    //        ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    //        playerCustomProperties["Role"] = playerRole.ToString();

    //        player.SetCustomProperties(playerCustomProperties);
    //    }
    //}
}
