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

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (ammo > 0) 
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
            ammo -= 1;
        }
    }

    public void Reload(int amount)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
    }
}
