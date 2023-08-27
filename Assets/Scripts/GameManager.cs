using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{

    public RoleAssigner roleAssigner;
    [SerializeField] GameObject Pared1;
    [SerializeField] GameObject Pared2;
    [SerializeField] GameObject Pared3;
    [SerializeField] AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // Hacer coroutine para destruir las paredes
        StartCoroutine("destruirParedes");

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }

    void respawn()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Prop")
        {
            Vector3 randomPosition2 = new Vector3(Random.Range(0, 10), 1, Random.Range(0, 10));
            PhotonNetwork.Instantiate(roleAssigner.playerPrefab.name, randomPosition2, Quaternion.identity);
            
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString() == "Hunter")  // hacer que el hunter se quede en espectador
        {
            
      
        }


}
  IEnumerator destruirParedes()
    {
        yield return new WaitForSeconds(15);
        audioSource.Play();
        Pared1.gameObject.SetActive(false);
        Pared2.gameObject.SetActive(false);
        Pared3.gameObject.SetActive(false);
    }
}
