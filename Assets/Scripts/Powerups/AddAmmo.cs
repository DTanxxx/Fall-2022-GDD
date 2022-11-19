using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmo : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Shooter>())
        {
            collision.gameObject.GetComponent<Shooter>().Reload();
            Destroy(gameObject);
        }
    }
}
