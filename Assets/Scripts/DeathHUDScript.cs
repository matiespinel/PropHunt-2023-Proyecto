using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHUDScript : MonoBehaviour
{
    private void ActivateDeathHUD()
    {
        Entity.OnEntityDeath += EnableDeathHUD;
    }

    private void DeactivateDeathHUD()
    {
        Entity.OnEntityDeath += DisableDeathHUD;
    }
    private void EnableDeathHUD()
    {
        this.gameObject.SetActive(true);
    }

    private void DisableDeathHUD()
    {
        this.gameObject.SetActive(false);
    }
    void Awake()
    {
        ActivateDeathHUD();
        DisableDeathHUD();
    }
}
