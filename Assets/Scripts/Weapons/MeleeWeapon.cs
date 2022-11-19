using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject meleeWeapon = null;
    [SerializeField] private GameObject parent = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale > 0f)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent != null && parent.GetComponent<EnemyMovement>())
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }
        else if (parent != null && parent.GetComponent<PlayerMovement>())
        {
            if (collision.gameObject.GetComponent<EnemyMovement>())
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }
        else if (parent == null)
        {
            parent = collision.gameObject;
            GameObject weapon = Instantiate(meleeWeapon, transform.position, Quaternion.identity, parent.transform);
        }
    }
}
