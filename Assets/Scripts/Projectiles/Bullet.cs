using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX = null;
    [SerializeField] private string tagToAvoid = "Player";
    [SerializeField] private string tagToAvoid2 = "Bullet";
    [SerializeField] private int damageDealt = 1;

    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        gameObject.layer = player.layer;

        string name = player.GetComponent<SpriteRenderer>().sortingLayerName;
        GetComponent<SpriteRenderer>().sortingLayerName = name;
        SpriteRenderer[] srs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = name;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToAvoid || collision.gameObject.tag == tagToAvoid2 || collision.gameObject.GetComponent<LayerTrigger>()) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt);
        }
            
        Destroy(vfx, 5f);
        Destroy(gameObject);
    }
}
