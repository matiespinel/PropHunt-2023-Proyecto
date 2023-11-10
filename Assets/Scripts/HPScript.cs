using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HPScript : Entity
{
    void Awake() {
        HP = 100;
        HPText.text = HP.ToString();
        view = GetComponent<PhotonView>();
    }
}