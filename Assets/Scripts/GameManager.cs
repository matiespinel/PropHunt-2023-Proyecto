using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using ExitGames.Client.Photon;



public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    
    public RoleManager roleManager;
    [SerializeField] GameObject Pared1;
    [SerializeField] GameObject Pared2;
    [SerializeField] GameObject Pared3;
    [SerializeField] AudioSource audioSource;
    // hacer un timer de 5 minutos
    public float timer = 300;
    bool timeIsRunning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Hacer coroutine para destruir las paredes
        StartCoroutine("destruirParedes");
        timeIsRunning = true;
        UpdatePropC();
        

    }
   

    // Update is called once per frame
    void Update()
    {
         if (RoleManager.hunterCount == 0)
        {
            Debug.Log("Props win");
            StartCoroutine("finalizarPartida");
            //a
        }if (RoleManager.propCount == 0)
        {
            Debug.Log("Hunter wins");
        StartCoroutine("finalizarPartida");
        }

       

        if (timeIsRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                
                timer = 0;
                timeIsRunning = false;
                StartCoroutine("finalizarPartida");
            }
        }
    }


   
  IEnumerator destruirParedes()
    {
        yield return new WaitForSeconds(15);
        Pared1.gameObject.SetActive(false);
        Pared2.gameObject.SetActive(false);
        Pared3.gameObject.SetActive(false);
        audioSource.Play();

    }
    IEnumerator finalizarPartida()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Saliendo de la partida");
    }
    public override void OnLeftRoom()
    {
          PhotonNetwork.LoadLevel("Lobby");
            Debug.Log("Disconnected");
    }
    private void UpdatePropC() => Entity.OnEntityDeath += PropC;
    public void PropC() => RoleManager.propCount -= 1;
   public void OnEvent(EventData photonEvent)
{
    RoleManager.EventCodes eventCode = (RoleManager.EventCodes)(int)photonEvent.Code;
    object[] data = (object[])photonEvent.CustomData;

    switch (eventCode)
    {
        case RoleManager.EventCodes.PropCountChange:
            RoleManager.propCount = (int)data[0];
            break;
        case RoleManager.EventCodes.HunterCountChange:
            RoleManager.hunterCount = (int)data[0];
            break;
    }
}
    }
