using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float ammo = 20f;
    [SerializeField] private float maxAmmo = 20f;
    [SerializeField] private int rechargeRate = 1;
    [SerializeField] private int rechargeDelay = 5;
    [SerializeField] private int chargeDelay = 1;

    private float rechargeStart = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Out of Ammo");
            }
        }
        Recharge();
    }

    private void Shoot()
    {
        if (ammo > 0) 
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
            ammo -= 1;
            rechargeStart = Time.time + rechargeDelay;
        }
    }

    private void Recharge()
    {
        if (Time.time > rechargeStart && ammo < maxAmmo)
        {
            ammo = Mathf.Min(maxAmmo, ammo + rechargeRate);
            rechargeStart = rechargeStart + chargeDelay;
            Debug.Log("+1 Ammo");
        }
    }

    public void Reload(int amount)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
    }
}
