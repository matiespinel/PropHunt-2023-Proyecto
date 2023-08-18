using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotScript : Weapon
{
    private Entity youEntity;
    void Start()
    {
        #region bullet Attributes
        bulletVelocity = 1;
        bulletDamage = 1;
        bulletRange = 100;
        fireRate = 0.25f;
        nextShotInterval = 0;
        bulletLine = GetComponent<LineRenderer>();
        fireButton = KeyCode.K;
        bulletTime = new WaitForSeconds(.07f);
        youEntity = this.gameObject.GetComponentInParent<Entity>();
        #endregion

    }

    void Update()
    {
        if(Input.GetKey(fireButton) && Time.time > nextShotInterval)
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
                    hit.collider.GetComponent<Entity>().TakeDamage(20);
                }
            }
            else
            {
                bulletLine.SetPosition(1,bulletOrigin + (transform.up * bulletRange));
                youEntity.TakeDamage(20);
                Debug.Log("pipi");
            }
        }
    }
/* [PunRCP] */
    private IEnumerator ShotEffect()
    {
        //bang sonido faltar
        RegisterShotAudio();
        bulletLine.enabled = true;
        yield return bulletTime;
        bulletLine.enabled = false;
        DeregisterShotAudio();
    }

    private void OnDestroy() {
        DeregisterShotAudio();
    }
}
