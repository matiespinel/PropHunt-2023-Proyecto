using UnityEngine;
using Photon.Pun;
using Photon.Realtime;




public class RoleAssigner : MonoBehaviourPunCallbacks
{
  
    Player[] allPlayer;
    int hunter;
    public GameManager gm;
 public enum PlayerRole
    {
       Cazador,
       Prop
    }

       public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float huntX;
    public float huntXmax;
    public float huntZ;
    public float huntZmax;
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    void Awake()
    {
      
    }
    void Start()
    {
        
        if ("Hunter" == PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString())
        {
            Vector3 randomPosition2 = new Vector3(Random.Range(huntX, huntXmax), 5, Random.Range(huntZ, huntZmax));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition2, Quaternion.identity);
            Debug.Log("Hunter");
            Debug.Log(RoleManager.hunterCount);

        }
        else if ("Prop" == PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString())
        {


            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 2, Random.Range(minZ, maxZ));
            PhotonNetwork.Instantiate(playerPrefab2.name, randomPosition, Quaternion.identity);
            Debug.Log("Prop");
            Debug.Log(RoleManager.propCount);


        }

        else 
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 2, Random.Range(minZ, maxZ));
            PhotonNetwork.Instantiate(playerPrefab2.name, randomPosition, Quaternion.identity);
            Debug.Log("No anda");
            Debug.Log(RoleManager.propCount);
            
        }

        Debug.Log(RoleManager.propCount);
        Debug.Log(RoleManager.hunterCount);
    }
}