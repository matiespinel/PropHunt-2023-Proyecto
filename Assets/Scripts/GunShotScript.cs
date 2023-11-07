using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunShotScript : Weapon
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
        bulletLine = GetComponentInChildren<LineRenderer>();
        fireButton = KeyCode.K;
        bulletTime = new WaitForSeconds(.07f);
        mag = 20;
        initialMag = mag;
        ammo = 100;
        reloadTime = new WaitForSeconds(5);
        youEntity = GetComponent<Entity>();
        animator = GetComponent<Animator>();
        #endregion

    }

    void FixedUpdate()
    {
        if(!photonView.IsMine) return;

        if(Input.GetKey(fireButton) && Time.time > nextShotInterval && cooldownReloadBool)
        {
            bulletOrigin = bulletLine.transform.position;
            nextShotInterval = Time.time + fireRate;

            RaycastHit hit;

            if(Physics.Raycast(bulletOrigin, bulletLine.transform.up,out hit, bulletRange))
            {
                bulletEnd = hit.point;
                StartCoroutine(ShotEffect());
                if (hit.collider.gameObject.layer == 6)//6 => layer llamada "Prop"
                {
                    hit.collider.GetComponent<PhotonView>()?.RPC("TakeDamage", RpcTarget.All, 20);
                }
                else photonView?.RPC("TakeDamage", RpcTarget.All, 20);
            }
            else
            {
                bulletEnd = bulletOrigin + (transform.up * bulletRange);
                photonView?.RPC("TakeDamage", RpcTarget.All, 20);
                StartCoroutine(ShotEffect());

            }
            
        }
        if (mag == 0 && cooldownReloadBool || Input.GetKey(KeyCode.R) && cooldownReloadBool && mag != initialMag)
        {
            StartCoroutine(ReloadWait());
        }
    }
}
