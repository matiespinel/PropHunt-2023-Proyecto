using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;

public abstract class Entity : MonoBehaviourPunCallbacks, IPunObservable
{
    #region attributes
    public int HP{get; set;}


    public TMP_Text HPText;
    #endregion
    public GameManager gm;
    public RoleAssigner roleAssigner;
    public PhotonView view;
    ///<summary>
    ///Accion que se invoca al morir
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
            HP = (int)stream.ReceiveNext();
            Debug.Log("vida:"+HP);
        }
    }
    [PunRPC]
    public void TakeDamage(int dmg) {
        HP -= dmg;
        HPText.text = HP.ToString();
        if(HP <= 0)
        {
            
            if (view.IsMine) 
            {
                PhotonNetwork.Destroy(transform.parent.gameObject);
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
            RoleManager.propCount--;
            PhotonNetwork.Destroy(this.gameObject);

        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Hunter")  // hacer que el hunter se quede en espectador
        {
            PhotonNetwork.Destroy(this.gameObject);
      
        }


}
}
