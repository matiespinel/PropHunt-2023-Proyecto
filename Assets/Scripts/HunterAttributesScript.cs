using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HunterAttributesScript : Entity
{
    void Start() {
        HP = 100;
        walkSpeed = 15;
        runSpeed = 30;
        view = GetComponent<PhotonView>();
    }
}
