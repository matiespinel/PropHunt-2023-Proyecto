using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public RoleManager roleManagerInstance;
    [SerializeField] GameObject Pared1;
    [SerializeField] GameObject Pared2;
    [SerializeField] GameObject Pared3;
    [SerializeField] GameObject Pared4;
    [SerializeField] GameObject Pared5;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Canvas PropsWin;
    [SerializeField] Canvas HunterWin;

    public float timer = 300;
    bool timeIsRunning = false;

     void Awake()
    {
        roleManagerInstance = InstantiateRoleManager();

        RegisterRoleManagerForCallbacks();
    }

    void Start()
    {
        StartCoroutine("destruirParedes");
        timeIsRunning = true;
        UpdatePropC();
        //decir por consola cual es el rol del jugador
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["Role"]?.ToString());
    }

    void Update()
    {
        if (RoleManager.hunterCount == 0)
        {
            Debug.Log("Props win");
            StartCoroutine("PropGanan");
        }
        if (RoleManager.propCount == 0)
        {
            Debug.Log("Hunter wins");
            StartCoroutine("CazadorGanan");
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
                StartCoroutine("PropsGanan");
            }
        }
    }

    IEnumerator destruirParedes()
    {
        yield return new WaitForSeconds(15);
        Pared1.gameObject.SetActive(false);
        Pared2.gameObject.SetActive(false);
        Pared3.gameObject.SetActive(false);
        Pared4.gameObject.SetActive(false);
        Pared5.gameObject.SetActive(true);
        audioSource.Play();
    }

    IEnumerator CazadorGanan()
    {
        HunterWin.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        PhotonNetwork.Disconnect();
        HunterWin.gameObject.SetActive(false);
        Debug.Log("Saliendo de la partida");
    }

    IEnumerator PropGanan()
    {
        PropsWin.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        PhotonNetwork.Disconnect();
        PropsWin.gameObject.SetActive(false);
        Debug.Log("Saliendo de la partida");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
        Debug.Log("Disconnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("Menu");
        Debug.Log("Disconnected");
    }

    private void UpdatePropC() => Entity.OnEntityDeath += PropC;

   public void PropC()
{
    RoleManager.propCount -= 1;
    RoleManager.rol.SendCountChange(RoleManager.EventCodes.PropCountChange, RoleManager.propCount);
}

     public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RoleManager.EventCodes.PropCountChange)
        {
            RoleManager.propCount = (int)photonEvent.CustomData;
        }
        else if (photonEvent.Code == (byte)RoleManager.EventCodes.HunterCountChange)
        {
            RoleManager.hunterCount = (int)photonEvent.CustomData;
        }
    }
    void OnEnable()
{
    base.OnEnable(); 
    PhotonNetwork.AddCallbackTarget(this);
}

void OnDisable()
{
    base.OnDisable(); 
    PhotonNetwork.RemoveCallbackTarget(this);
}


    void SendCountChange(RoleManager.EventCodes eventCode, int value)
{
    RoleManager.rol.SendCountChange(eventCode, value);
}

 private RoleManager InstantiateRoleManager()
    {
        // Lógica para instanciar tu RoleManager (puedes ajustar esto según tu estructura de proyecto)
        GameObject roleManagerGO = new GameObject("RoleManager");
        RoleManager roleManager = roleManagerGO.AddComponent<RoleManager>();
        return roleManager;
    }

    private void RegisterRoleManagerForCallbacks()
    {
        // Registrar RoleManager para recibir eventos
        PhotonNetwork.AddCallbackTarget(roleManagerInstance);
    }

    private void UnregisterRoleManagerFromCallbacks()
    {
        // Desregistrar RoleManager cuando ya no es necesario
        PhotonNetwork.RemoveCallbackTarget(roleManagerInstance);
    }
}
