using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject pistolPrefab = null;
    [SerializeField] private GameObject minigunPrefab = null;
    private Gun currentGun;

    private void Start() {
        currentGun = new Pistol(firingOrigin, bulletPrefab, pistolPrefab);
    }

    private void Update() {
        currentGun.Update();
    }

    public void Reload() {
        currentGun.Reload();
    }

    public void switchMinigun() {
        currentGun.drop();
        currentGun = new Minigun(firingOrigin, bulletPrefab, minigunPrefab, FindObjectOfType<GameManager>().minigunAmmo);
        FindObjectOfType<CurrentGunUI>().switchMinigun();
    }

    public void switchPistol() {
        currentGun.drop();
        currentGun = new Pistol(firingOrigin, bulletPrefab, pistolPrefab);
        FindObjectOfType<CurrentGunUI>().switchPistol();
    }

    public int ammoCount() {
        return currentGun.ammoCount();
    }

    public int maxAmmoCount() {
        return currentGun.maxAmmoCount();
    }
}
