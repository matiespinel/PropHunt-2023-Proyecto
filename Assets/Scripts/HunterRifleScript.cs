using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;

public class HunterRifleScript : Weapon
{
    private Entity youEntity;

    void Awake()
    {
        #region bullet Attributes
        
        cooldownReloadBool = true;
        bulletVelocity = 1;
        bulletDamage = 1;
        bulletRange = 100;
        fireRate = 0.25f;
        nextShotInterval = 0;
        
        fireButton = KeyCode.K;
        bulletTime = new WaitForSeconds(.07f);
        mag = 20;
        initialMag = mag;
        ammo = 100;
        reloadTime = new WaitForSeconds(2.8f);
       
        #endregion
        youEntity = GetComponent<Entity>();
        animator = GetComponent<Animator>();
        bulletLine = GetComponentInChildren<LineRenderer>();
        _audioSource = GetComponentInChildren<AudioSource>();
        MuzzleFlash = GetComponentInChildren<ParticleSystem>();
        rigBuilder = GetComponent<RigBuilder>();

    }

    void FixedUpdate()
    {
        if(!photonView.IsMine) enabled = false;

        if(Input.GetKey(fireButton) && Time.time > nextShotInterval && cooldownReloadBool)
        {
            bulletOrigin = bulletLine.transform.position;
            nextShotInterval = Time.time + fireRate;

            RaycastHit hit;

            if(Physics.Raycast(bulletOrigin, bulletLine.transform.up,out hit, bulletRange))
            {
                bulletEnd = hit.point;
                StartCoroutine(ShotEffect());
                if (hit.collider.gameObject.layer == 7)//6 => layer llamada "PropHuman"
                {
                    hit.collider.GetComponent<PhotonView>()?.RPC("TakeDamage", RpcTarget.All, 20);
                }
                else photonView?.RPC("TakeDamage", RpcTarget.All, 10);
            }
            else
            {
                bulletEnd = bulletOrigin + (transform.up * bulletRange);
                photonView?.RPC("TakeDamage", RpcTarget.All, 10);
                StartCoroutine(ShotEffect());

            }
            
        }
        if (mag == 0 && cooldownReloadBool || Input.GetKey(KeyCode.R) && cooldownReloadBool && mag != initialMag)
        {
            StartCoroutine(ReloadWait());
            
        }
    }
}
