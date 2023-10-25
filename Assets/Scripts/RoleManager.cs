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
    [SerializeField] int minPlayers;
    

    public static int propCount { get;  set; } = 0;
    public static int hunterCount  { get; set; } = 0;
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
                            hunterCount++;
                            Debug.Log(hunterCount);
                }
                else
                {
                       PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                            propCount++;
                            Debug.Log(propCount);
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
                            hunterCount++;
                        }
                        else
                        {
                             p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                              propCount++;
                        }
                    }
            }
           

        }
    
}
