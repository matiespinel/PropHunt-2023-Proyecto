using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    public int propCount = 0;
    public int hunterCount = 0;
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
        if (propCount == 0)
        {
           
        }

        if (hunterCount == 0)
        {
            // finalizar partida mediante coroutine

            
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
    IEnumerator finalizarPartida()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
    }
