using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerItems : MonoBehaviour
{
    public TMP_Text playerName;
    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }


}
