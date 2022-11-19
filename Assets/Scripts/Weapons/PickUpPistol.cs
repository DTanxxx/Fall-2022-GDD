using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPistol : MonoBehaviour
{
    private float timer = 1f;

    private void Start() {
        timer += Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time < timer) { return; }

        if (collision.gameObject.GetComponent<Shooter>())
        {
            collision.gameObject.GetComponent<Shooter>().switchPistol();
            Destroy(gameObject);
        }
    }
}
