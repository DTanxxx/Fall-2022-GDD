using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun
{
    public Transform firingOrigin = null;
    public GameObject bulletPrefab = null;
    public GameObject minigunPrefab = null;
    public float bulletForce = 20f;
    public new int ammo = 50;
    public new int maxAmmo = 50;
    private float fireDelay = 0.05f;
    public new int reloadAmount = 50;

    private float fireDelayStart = 0f;

    public Minigun(Transform fo, GameObject bp, GameObject mp, int startammo) {
        firingOrigin = fo;
        bulletPrefab = bp;
        minigunPrefab = mp;
        ammo = startammo;
    }
    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButton("Fire1"))
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
    }

    public void Shoot()
    {
        if (Time.time < fireDelayStart) {return;}
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo -= 1;
        fireDelayStart = Time.time + fireDelay;
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
        //Debug.Log("in minigun drop");
        Instantiate(minigunPrefab, firingOrigin.position, firingOrigin.rotation);
        FindObjectOfType<GameManager>().minigunAmmo = ammo;
    }
}
