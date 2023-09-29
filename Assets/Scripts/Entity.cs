using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public abstract class Entity : MonoBehaviour
{
    #region attributes
    public int HP{get; set;}
    public int walkSpeed{get; set;}
    public int runSpeed{get; set;}
    #endregion
    public GameManager gm;
    public RoleAssigner roleAssigner;
    ///<summary>
    ///Funcion que inserta el valor y le resta a la HP el parametro dmg
    ///</summary>
    
    public static event Action OnEntityDeath;
    public void TakeDamage(int dmg) {
        HP -= dmg;
        Debug.Log(HP);
        if(HP <= 0)
        {
            respawn();
            OnEntityDeath?.Invoke();//Este evento permitira conectar scripts que se "activaran" al momento de la muerte. Usar esto para respawn
        }
    }

     void respawn()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Prop")
        {
            Vector3 randomPosition2 = new Vector3(UnityEngine.Random.Range(0, 10), 1, UnityEngine.Random.Range(0, 10));
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
            PhotonNetwork.Instantiate(roleAssigner.playerPrefab.name, randomPosition2, Quaternion.identity);
            gm.propCount--;
            
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Hunter")  // hacer que el hunter se quede en espectador
        {
            Destroy(this.gameObject);
      
        }


}
}
