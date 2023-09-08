using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class RoleManager : MonoBehaviour
{   
    // make this GameObject a singleton
    public static RoleManager rol;
    Player[] allPlayer;
    int hunter; 
    public GameManager gm;
    void Start()
    {
        // if there is no instance of this class
        if (rol == null)
        {
            // make this the instance
            rol = this;
            // make this object persist between scenes
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

   
    public void RoleAssigner()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                       PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
                }
                else if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                       PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                       gm.propCount++;
                }

            }
            else{
                 allPlayer = PhotonNetwork.PlayerList;
        hunter = Random.Range(0, allPlayer.Length - 1);
        foreach (Player p in allPlayer)
        {
            if (p.ActorNumber == hunter)
            {
                p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
            }
            else
            {
                p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                gm.propCount++;
            }
        }
            }
           

        }
    
}
