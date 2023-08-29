using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
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
    public float bulletVelocity {get; set;}
    public float bulletDamage {get; set;}
    public float bulletRange {get; set;}
    public float fireRate {get; set;}
    public KeyCode fireButton  {get; set;}
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
    public IEnumerator ShotEffect()
    {
        ammo--;
        Debug.Log(ammo);
        RegisterShotAudio();
        bulletLine.enabled = true;
        yield return bulletTime;
        bulletLine.enabled = false;
        DeregisterShotAudio();
    }
    /// <summary>
    /// Tiempo de recarga. 
    /// </summary>
    public IEnumerator ReloadWait()
    {
        Debug.Log("recargando...");
        yield return reloadTime;
        ammo = mag;
    }
    //¡Si alguien se muere por disparos sin esto el audio queda registrado!
    private void OnDestroy() =>DeregisterShotAudio();
}
