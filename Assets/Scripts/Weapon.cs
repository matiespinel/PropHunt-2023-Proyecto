using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public abstract class Weapon : MonoBehaviourPunCallbacks
{
    
    public enum type
    {
        GunShotScript,
        gun2,
    }
    private AudioSource _audioSource;
    #region WeaponData
    public Vector3 bulletOrigin;
    public LineRenderer bulletLine;
    public WaitForSeconds bulletTime;
    public WaitForSeconds reloadTime;

    public int ammo {get; set;}
    public float nextShotInterval {get; set;}
    public int mag {get; set;}
    public int initialMag { get; set;}
    public float bulletVelocity {get; set;}
    public float bulletDamage {get; set;}
    public float bulletRange {get; set;}
    public float fireRate {get; set;}

    public bool cooldownReloadBool { get; set; }
    public KeyCode fireButton  {get; set;}

    public TMP_Text ammoCounter;
    public Animator animator;
    #endregion WeaponData    
    private void Awake() => _audioSource = GetComponent<AudioSource>();
    /// <summary>
    /// Registra el clip del disparo de arma
    /// </summary>
    public void RegisterShotAudio() => SoundManagerScript.OnAnySound += PlayGunShotAudio;
    /// <summary>
    /// Desregistra el clip del disparo de arma
    /// </summary>
    public void DeregisterShotAudio() => SoundManagerScript.OnAnySound -= PlayGunShotAudio;
    /// <summary>
    /// Sonido al disparar arma
    /// </summary>
    public void PlayGunShotAudio() => _audioSource.Play();
    /// <summary>
    /// Efecto de disparo. Registro y Deresgistro de audioclip, resta de balas y tiempo entre disparos.
    /// </summary>
    
    public void UpdateAmmoCounter() 
    {
        ammoCounter.text = mag+"/"+ammo;
    }
    public IEnumerator ShotEffect()
    {
        mag--;
        Debug.Log(mag);
        animator.SetBool("isFiring", true);
        RegisterShotAudio();
        bulletLine.enabled = true;
        yield return bulletTime;
        UpdateAmmoCounter();
        bulletLine.enabled = false;
        animator.SetBool("isFiring", false);

        DeregisterShotAudio();
    }
    /// <summary>
    /// Tiempo de recarga. 
    /// </summary>
    public IEnumerator ReloadWait()
    {
        cooldownReloadBool = false;
        Debug.Log("recargando...");
        Debug.Log(initialMag);
        animator.SetBool("isReloading", true);
        yield return reloadTime;
        animator.SetBool("isReloading", false);
        mag += initialMag;
        ammo -= initialMag;
        UpdateAmmoCounter();
        cooldownReloadBool = true;
    }
    //¡Si alguien se muere por disparos sin esto el audio queda registrado!
    private void OnDestroy() =>DeregisterShotAudio();
}
