using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPistol : MonoBehaviour
{
    private bool firstTime = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        if (collision.gameObject.GetComponent<Shooter>())
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Shooter>().switchPistol();
        }
    }
}
