using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;




public class RoleAssigner : MonoBehaviourPunCallbacks
{
  
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
    public GameObject playerPrefab2;
    void Awake()
    {
      
    }
    void Start()
    {
        
        if ("Hunter" == PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString())
        {
            Vector3 randomPosition2 = new Vector3(Random.Range(min2X, max2X), 2, Random.Range(min2Z, max2Z));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition2, Quaternion.identity);
            Debug.Log("Hunter");
        }
        else if ("Prop" == PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString())
        {


            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 2, Random.Range(minZ, maxZ));
            PhotonNetwork.Instantiate(playerPrefab2.name, randomPosition, Quaternion.identity);
            Debug.Log("Prop");

        }

        else 
        {
            PhotonNetwork.Instantiate(playerPrefab2.name, randomPosition2, Quaternion.identity);
            Debug.Log("No anda");
        }
    }
}