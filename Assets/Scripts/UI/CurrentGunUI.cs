using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGunUI : MonoBehaviour
{
    [SerializeField] public Sprite[] guns;
    [SerializeField] private Image display;
    private Sprite activeGun;
    
    private void Start() {
        activeGun = guns[0];
    }

    private void Update() {
        display.sprite = activeGun;
    }

    public void switchPistol() {
        activeGun = guns[0];
    }

    public void switchMinigun() {
        activeGun = guns[1];
    }
}
