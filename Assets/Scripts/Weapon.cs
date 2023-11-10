using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Animations.Rigging;

public abstract class Weapon : MonoBehaviourPunCallbacks
{
    
    public enum type
    {
        GunShotScript,
        gun2,
    }
    public RigBuilder rigBuilder;

    #region WeaponData
    public Vector3 bulletOrigin;
    public Vector3 bulletEnd;
    public LineRenderer bulletLine;
    public WaitForSeconds bulletTime;
    public WaitForSeconds reloadTime;
    public AudioSource _audioSource;
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
    public ParticleSystem MuzzleFlash;
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
    
    
    public void UpdateAmmoCounter() 
    {
        ammoCounter.text = mag+"/"+ammo;
    }
    /// <summary>
    /// Efecto de disparo. Registro y Deresgistro de audioclip, resta de balas y tiempo entre disparos.
    /// </summary>
    public IEnumerator ShotEffect()
    {
        mag--;
        photonView.RPC("ShootEffectStart", RpcTarget.All, bulletEnd);
        yield return bulletTime;
        UpdateAmmoCounter();
        photonView.RPC("ShootEffectEnd", RpcTarget.All);
    }
    /// <summary>
    /// Tiempo de recarga. 
    /// </summary>
    public IEnumerator ReloadWait()
    {
        cooldownReloadBool = false;
        photonView.RPC("SetReloadEffectBool", RpcTarget.All, true);
        
        yield return new WaitForSeconds(0.2f);

        photonView.RPC("RigBuilderState", RpcTarget.All, false);
        photonView.RPC("SetReloadEffectBool", RpcTarget.All, false);

        yield return reloadTime;

        photonView.RPC("RigBuilderState", RpcTarget.All, true);
        var dif = initialMag - mag;
        mag += dif;
        ammo -= dif;
        UpdateAmmoCounter();
        cooldownReloadBool = true;
    }
    //�Si alguien se muere por disparos sin esto el audio queda registrado!
    private void OnDestroy() =>DeregisterShotAudio();

    [PunRPC]
    public void SetReloadEffectBool(bool boolean)
    {
        animator.SetBool("isReloading", boolean);
    }
    [PunRPC]
    public void RigBuilderState(bool boolean)
    {
        rigBuilder.enabled = boolean;
    }
    [PunRPC]
    public void ShootEffectStart(Vector3 linend)
    {
        animator.SetBool("isFiring", true);
        RegisterShotAudio();
        MuzzleFlash.Play();
        bulletLine.enabled = true;
        bulletLine.SetPosition(0, bulletLine.transform.position);
        bulletLine.SetPosition(1, linend);
    }
    [PunRPC]
    public void ShootEffectEnd()
    {
        bulletLine.enabled = false;
        animator.SetBool("isFiring", false);
        DeregisterShotAudio();
    }
}
