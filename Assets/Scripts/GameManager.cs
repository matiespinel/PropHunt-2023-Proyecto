using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    
    public RoleManager roleManager;
    [SerializeField] GameObject Pared1;
    [SerializeField] GameObject Pared2;
    [SerializeField] GameObject Pared3;
    [SerializeField] AudioSource audioSource;
    // hacer un timer de 5 minutos
    [SerializeField] float timer = 300;
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
        if (RoleManager.propCount == 0)
        {
           
        }

        if (RoleManager.hunterCount == 0)
        {
            // finalizar partida mediante coroutine

            
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
    private void UpdatePropC() => Entity.OnEntityDeath += PropC;
    public void PropC() => RoleManager.propCount -= 1;
    }
