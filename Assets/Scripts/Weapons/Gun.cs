using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    public int reloadAmount;
    public int maxAmmo;

    // Update is called once per frame
    public virtual void Update()
    {
        //to be overwritten
    }

    public virtual void Reload()
    {
        ammo = Mathf.Min(ammo + reloadAmount, maxAmmo);
    }

    public virtual int ammoCount() {
        return ammo;
    }

    public virtual int maxAmmoCount() {
        return maxAmmo;
    }

    public virtual void drop() {
        //to be overriden
    }
}
