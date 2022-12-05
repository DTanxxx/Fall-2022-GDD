using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        Debug.Log("tag" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            collision.GetContacts(contacts);
            Vector3 normal = contacts[0].normal;
            Vector2 point = contacts[0].point;
            Debug.Log("point" + point);
            transform.Translate(Vector3.right);
        }
    }
}
