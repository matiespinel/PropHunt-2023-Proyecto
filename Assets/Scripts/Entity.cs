using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class Entity : MonoBehaviour
{
    #region attributes
    public int HP{get; set;}
    public int walkSpeed{get; set;}
    public int runSpeed{get; set;}
    #endregion
    ///<summary>
    ///Funcion que inserta el valor y le resta a la HP el parametro dmg
    ///</summary>
    ///
    [PunRPC]
    public void TakeDamage(int dmg) {
        HP -= dmg;
        Debug.Log(HP);
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
