using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float min2X;
    public float max2X;
    public float min2Z;
    public float max2Z;


    public void Start()
    {
        //if (RoleAssigner.rol.Roles == 0)
        //{
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //}
        
        //else
        //{
        //    Vector3 randomPosition = new Vector3(Random.Range(min2X, max2X), 0, Random.Range(min2Z, max2Z));
        //    PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //}
    }

    
}
