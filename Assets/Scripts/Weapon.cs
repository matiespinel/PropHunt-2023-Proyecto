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

    public int ammo {get; set;}
    public float nextShotInterval {get; set;}
    public int maxAmmo {get; set;}
    public float bulletVelocity {get; set;}
    public float bulletDamage {get; set;}
    public float bulletRange {get; set;}
    public float fireRate {get; set;}

}
