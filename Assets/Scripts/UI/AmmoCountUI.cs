using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    private Sprite activeGun;
    private Shooter shooter;

    private void Start() {
        shooter = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<Shooter>();
    }

    private void Update() {
        ammoDisplay.text = "Ammo: " + shooter.ammoCount().ToString()
            + "/" + shooter.maxAmmoCount().ToString();
    }
}

