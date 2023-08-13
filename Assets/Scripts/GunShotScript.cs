using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotScript : Weapon
{
    Vector3 bulletOrigin;
    /* [SerializeField] float bulletVelocity;
    
    [SerializeField] float bulletDamage;
    [SerializeField] int bulletRange;
    [SerializeField] float fireRate;
    [SerializeField] float nextShotInterval; */
    WaitForSeconds bulletTime = new WaitForSeconds(.07f);
    [SerializeField] KeyCode fireButton;

    private LineRenderer bulletLine;

    
    void Start()
    {
        bulletVelocity = 1;
        bulletDamage = 1;
        bulletRange = 100;
        fireRate = 0.25f;
        nextShotInterval = 0;
        bulletLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(Input.GetKey(fireButton) && Time.time > nextShotInterval)
        {
            nextShotInterval = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            RaycastHit hit;bulletOrigin = transform.position;
            bulletLine.SetPosition(0,bulletOrigin);

            if(Physics.Raycast(bulletOrigin, transform.up,out hit, bulletRange))
            {
                bulletLine.SetPosition(1, hit.point);
            }
            else
            {
                bulletLine.SetPosition(1,bulletOrigin + (transform.up * bulletRange));
                Debug.Log("pipi");
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        //bang sonido faltar
        bulletLine.enabled = true;
        yield return bulletTime;
        bulletLine.enabled = false;

    }
}
