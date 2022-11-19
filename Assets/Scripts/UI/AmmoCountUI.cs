using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountUI : MonoBehaviour
{
    [SerializeField] private Text ammoDisplay;
    private Sprite activeGun;
    private Shooter shooter;

    private void Start() {
        shooter = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<Shooter>();
    }

    private void Update() {
        ammoDisplay.text = shooter.ammoCount() + "/" + shooter.maxAmmoCount();
    }
}

