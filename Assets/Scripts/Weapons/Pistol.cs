using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Transform firingOrigin = null;
    public GameObject bulletPrefab = null;
    public GameObject pistolPrefab = null;
    public float bulletForce = 20f;
    public new int ammo = 20;
    public new int maxAmmo = 20;
    private int rechargeRate = 1;
    private int rechargeDelay = 5;
    private float chargeDelay = 0.5f;
    public new int reloadAmount = 10;

    private float rechargeStart = 0f;

    public Pistol(Transform fo, GameObject bp, GameObject pp) {
        firingOrigin = fo;
        bulletPrefab = bp;
        pistolPrefab = pp;
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                Shoot();
            }
        else
            {
                //Debug.Log("Out of Ammo");
            }
        }
        Recharge();
    }

    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo -= 1;
        rechargeStart = Time.time + rechargeDelay;

    }

    private void Recharge()
    {
        if (Time.time > rechargeStart && ammo < maxAmmo)
        {
            ammo = Mathf.Min(maxAmmo, ammo + rechargeRate);
            rechargeStart = Time.time + chargeDelay;
            //Debug.Log("+1 Ammo");
        }
    }

    public override void Reload() {
        ammo = Mathf.Min(ammo + reloadAmount, maxAmmo);
    }

    public override int ammoCount() {
        return ammo;
    }

    public override int maxAmmoCount() {
        return maxAmmo;
    }

    public override void drop() {
        Instantiate(pistolPrefab, firingOrigin.position, firingOrigin.rotation);
    }
}
