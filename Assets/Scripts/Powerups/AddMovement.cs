using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMovement : MonoBehaviour
{
    [SerializeField] private float speedBoost = 3f;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().SpeedUp(speedBoost);
            Destroy(gameObject);
        }
    }
}
