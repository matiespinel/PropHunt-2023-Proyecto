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
       
        
        
           allPlayers = PhotonNetwork.PlayerList;
            int hunter = Random.Range(0, allPlayers.Length);

                for (int i = 0; i < allPlayers.Length; i++)
                    {
                         Player player = allPlayers[i];
                      if (i == hunter)
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
