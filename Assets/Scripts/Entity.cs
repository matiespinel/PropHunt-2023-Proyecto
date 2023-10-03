using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public abstract class Entity : MonoBehaviourPunCallbacks, IPunObservable
{
    #region attributes
    public int HP{get; set;}
    public int walkSpeed{get; set;}
    public int runSpeed{get; set;}
    #endregion
    public GameManager gm;
    public RoleAssigner roleAssigner;
    public PhotonView view;
    ///<summary>
    ///Funcion que inserta el valor y le resta a la HP el parametro dmg
    ///</summary>
    
    public static event Action OnEntityDeath;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(HP);
        }
        else
        {
            // Network player, receive data
            HP = (int)stream.ReceiveNext();Debug.Log(HP);
            if(HP <= 0) 
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
            
        }
    }

    public void TakeDamage(int dmg) {
        HP -= dmg;
        
        if(HP <= 0)
        {
            
            if (view.IsMine) 
            {
                
                OnEntityDeath?.Invoke();//Este evento permitira conectar scripts que se "activaran" al momento de la muerte. Usar esto para respawn
            }

        }
    }
    [PunRPC]
     void respawn()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Prop")
        {
            Vector3 randomPosition2 = new Vector3(UnityEngine.Random.Range(0, 10), 1, UnityEngine.Random.Range(0, 10));
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
            PhotonNetwork.Instantiate(roleAssigner.playerPrefab.name, randomPosition2, Quaternion.identity);
            gm.propCount--;
            PhotonNetwork.Destroy(this.gameObject);

        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Hunter")  // hacer que el hunter se quede en espectador
        {
            PhotonNetwork.Destroy(this.gameObject);
      
        }


}
}
