using Cainos.PixelArtTopDown_Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanBullet : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damageDealt = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyShaman>() || collision.gameObject.tag == tagToAvoid || collision.gameObject.GetComponent<LayerTrigger>()) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().setImmuneState();
        }
            
        Destroy(vfx, 5f);
        Destroy(gameObject);
    }
}
