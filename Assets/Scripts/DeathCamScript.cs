using System.Collections;
using UnityEngine;
using Cinemachine;

public class DeathCamScript : MonoBehaviour
{
    #region vars
    [SerializeField]
    private GameObject[] players;
    private CinemachineFreeLook deathCam;
    [SerializeField]
    private int camIndex = 0;
    [SerializeField]
    private bool cooldownBool = true;
    private Camera normalCam;
    #endregion
    void Start()
    {
        normalCam = GetComponent<Camera>();
        deathCam = GetComponent<CinemachineFreeLook>();
        ActivateDeathCam();
        DisableDeathCam();
    }
    private void DeactivateDeathCam() => Entity.OnEntityDeath -= EnableDeathCam;
    private void DisableDeathCam()
    {
        deathCam.enabled = false;
        normalCam.enabled = false;
    }
    private void ActivateDeathCam() => Entity.OnEntityDeath += EnableDeathCam;
    private void EnableDeathCam()
    {
        deathCam.enabled = true;
        normalCam.enabled = true;
        players = GameObject.FindGameObjectsWithTag("DCamAttach");
        SwitchNext();
    }


    public void SwitchPrevious() 
    {
        if (cooldownBool)
                    {
                        cooldownBool = false;
                        if (camIndex > 0)
                        {
                            StartCoroutine(SwitchSpectatorCam(-1));
                        }
                        else 
                        { 
                            camIndex = players.Length - 1;
                            StartCoroutine(SwitchSpectatorCam(0));
                        }

                    }
        deathCam.LookAt = players[camIndex].transform;
        deathCam.Follow = players[camIndex].transform;
    }

    public void SwitchNext() 
    {
        if (cooldownBool) 
                    {
                        cooldownBool = false;
                        if(camIndex < players.Length - 1) 
                        {
                            StartCoroutine(SwitchSpectatorCam(1));
                        }
                        else 
                        { 
                            camIndex = 0;
                            StartCoroutine(SwitchSpectatorCam(0));
                        }
            
                    }
        deathCam.LookAt = players[camIndex].transform;
        deathCam.Follow = players[camIndex].transform;
    }
    private IEnumerator SwitchSpectatorCam(int direction) 
    {
        camIndex += direction;
        yield return new WaitForSeconds(2);
        cooldownBool = true;
    }
}
