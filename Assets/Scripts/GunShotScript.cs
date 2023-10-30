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
        bulletLine = GetComponent<LineRenderer>();
        fireButton = KeyCode.K;
        bulletTime = new WaitForSeconds(.07f);
        mag = 20;
        initialMag = mag;
        ammo = 100;
        reloadTime = new WaitForSeconds(5);
        youEntity = gameObject.GetComponentInParent<Entity>();
        animator = gameObject.GetComponentInParent<Animator>();
        #endregion

    }

    void FixedUpdate()
    {
        if(Input.GetKey(fireButton) && Time.time > nextShotInterval && mag != 0)
        {
            nextShotInterval = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            RaycastHit hit;
            bulletOrigin = transform.position;
            bulletLine.SetPosition(0,bulletOrigin);

            if(Physics.Raycast(bulletOrigin, transform.up,out hit, bulletRange))
            {
                bulletLine.SetPosition(1, hit.point);
                if(hit.collider.tag == "Transformable")
                {
                    hit.collider.GetComponent<PhotonView>()?.RPC("TakeDamage", RpcTarget.All, 20);
                }
            }
            else
            {
                bulletLine.SetPosition(1,bulletOrigin + (transform.up * bulletRange));
                animator.GetComponent<PhotonView>()?.RPC("TakeDamage", RpcTarget.All, 20);

            }
        }
        if (mag == 0 && cooldownReloadBool || Input.GetKey(KeyCode.R) && cooldownReloadBool && mag != initialMag)
        {
            StartCoroutine(ReloadWait());
        }
    }
}
