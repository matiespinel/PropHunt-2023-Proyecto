using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class RoomItem : MonoBehaviourPunCallbacks
{
    public Text roomName;
    CreateAndJoinRooms manager;

    private void Start()
    {
        manager = FindObjectOfType<CreateAndJoinRooms>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }

   
}
