using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RoleManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    // Make this GameObject a singleton
    public static RoleManager rol;

    Player[] allPlayers;
    int hunter;

    public static int propCount { get;  set; } = 0;
    public static int hunterCount { get; set; } = 0;

    public enum EventCodes : byte
    {
        PropCountChange,
        HunterCountChange
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == (byte)EventCodes.PropCountChange)
        {
            object[] data = (object[])photonEvent.CustomData;
            propCount = (int)data[0];
        }
        else if (eventCode == (byte)EventCodes.HunterCountChange)
        {
            object[] data = (object[])photonEvent.CustomData;
            hunterCount = (int)data[0];
        }
    }

    void Start()
    {
        if (rol == null)
        {
            rol = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendCountChange(EventCodes eventCode, int value)
    {
        byte eventCodeByte = (byte)eventCode;
        object[] content = new object[] { value };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(eventCodeByte, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void RoleAssigner()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
                hunterCount++;
                SendCountChange(EventCodes.HunterCountChange, hunterCount);
                Debug.Log(hunterCount);
            }
            else
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                propCount++;
                SendCountChange(EventCodes.PropCountChange, propCount);
                Debug.Log(propCount);
            }
        }
        else
        {
            allPlayers = PhotonNetwork.PlayerList;
            hunter = Random.Range(0, allPlayers.Length);

            foreach (Player player in allPlayers)
            {
                if (player.ActorNumber == hunter)
                {
                    player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
                    hunterCount++;
                    SendCountChange(EventCodes.HunterCountChange, hunterCount);
                }
                else
                {
                    player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                    propCount++;
                    SendCountChange(EventCodes.PropCountChange, propCount);
                }
            }

            bool isHunterAssigned = false;

            foreach (Player player in allPlayers)
            {
                if (player.CustomProperties.ContainsKey("Role") && player.CustomProperties["Role"].ToString() == "Hunter")
                {
                    Debug.Log("Hunter: " + player.NickName);
                    isHunterAssigned = true;
                }
            }

            if (!isHunterAssigned && PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
                hunterCount++;
                SendCountChange(EventCodes.HunterCountChange, hunterCount);
                Debug.Log("Hunter: " + PhotonNetwork.LocalPlayer.NickName);
            }
        }
    }

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
