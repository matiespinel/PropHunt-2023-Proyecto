using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;



public class RoleManager : MonoBehaviourPunCallbacks
{   
    // make this GameObject a singleton
    public static RoleManager rol;
    Player[] allPlayer;
    int hunter; 
    [SerializeField] int minPlayers;
    

    public static int propCount { get;  set; } = 0;
    public static int hunterCount  { get; set; } = 0;
    void Start()
    {
        // if there is no instance of this class
        if (rol == null)
        {
            // make this the instance
            rol = this;
            // make this object persist between scenes
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    public enum EventCodes : int
{
    PropCountChange,
    HunterCountChange
}

void SendCountChange(EventCodes eventCode, int value)
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
            else{
                 allPlayer = PhotonNetwork.PlayerList;
                 hunter = Random.Range(0, allPlayer.Length);
                    foreach (Player p in allPlayer)
                    {
                        if (p.ActorNumber == hunter)
                        {
                            p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Hunter" } });
                            hunterCount++;
                            SendCountChange(EventCodes.HunterCountChange, hunterCount);
                        }
                        else
                        {
                             p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Prop" } });
                            propCount++;
                            SendCountChange(EventCodes.PropCountChange, propCount);
                        }
                    }
                    bool isHunterAssigned = false;

foreach (Player p in allPlayer)
{
    if (p.CustomProperties.ContainsKey("Role") && p.CustomProperties["Role"].ToString() == "Hunter")
    {
        Debug.Log("Hunter: " + p.NickName);
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
    
}
