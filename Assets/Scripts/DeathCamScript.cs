using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class DeathCamScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    private CinemachineFreeLook deathCam;
    [SerializeField]
    private int camIndex = 0;
    private bool cooldownBool = true;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        deathCam = GetComponent<CinemachineFreeLook>();
        ActivateDeathCam();
        DisableDeathCam();
    }
    private void DeactivateDeathCam() => Entity.OnEntityDeath += DisableDeathCam;
    private void DisableDeathCam() => deathCam.enabled = false;
    private void ActivateDeathCam() => Entity.OnEntityDeath += EnableDeathCam;
    private void EnableDeathCam() => deathCam.enabled = true;

    private void Update()
    {
        if (deathCam) 
        {
            deathCam.LookAt = players[camIndex].transform;
            deathCam.Follow = players[camIndex].transform;
            if (Input.GetKey(KeyCode.D) && cooldownBool == true) 
            {
                cooldownBool = false;
                if(camIndex < players.Length - 1) 
                {
                    StartCoroutine(SwitchSpectatorCam(1));
                }
                else { camIndex = 0;}
            
            }
            if (Input.GetKey(KeyCode.A) && cooldownBool == true)
            {
                cooldownBool = false;
                if (camIndex > 0)
                {
                    StartCoroutine(SwitchSpectatorCam(-1));
                }
                else { camIndex = players.Length - 1;}

            }
        }

    }

    private IEnumerator SwitchSpectatorCam(int direction) 
    {
        camIndex += direction;
        yield return new WaitForSeconds(2);
        cooldownBool = true;
    }
}
