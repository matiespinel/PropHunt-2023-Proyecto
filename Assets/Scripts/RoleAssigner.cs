using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;




public class RoleAssigner : MonoBehaviourPunCallbacks
{
    
    public static RoleAssigner rol;
    Player[] allPlayer;
    int hunter;
 public enum PlayerRole
    {
       Cazador,
       Prop
    }

       public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float min2X;
    public float max2X;
    public float min2Z;
    public float max2Z;
    public GameObject playerPrefab;
    void Awake()
    {
        allPlayer = PhotonNetwork.PlayerList;
        hunter = Random.Range(0, allPlayer.Length);
        foreach (Player p in allPlayer)
        {
            if (p.ActorNumber == hunter)
            {
                p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
            }
            else
            {
                p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
            }
        }
        {

        }
    }
    void Start()
    {
       if ("Hunter" == PhotonNetwork.LocalPlayer.CustomProperties["Role"].ToString())
        {

       
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
      
        }
        else
        {
            Vector3 randomPosition2 = new Vector3(Random.Range(min2X, max2X), 0, Random.Range(min2Z, max2Z));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition2, Quaternion.identity);
        }
}
}