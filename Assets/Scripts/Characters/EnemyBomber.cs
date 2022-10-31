using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    [SerializeField] private GameObject hitVFX = null;

    private bool lockOn = false;

    protected override void Start()
    {
        base.Start();
        attackRange = 0.0f;
        speedMultiplier = 2.0f;
        damage = 5;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void CheckPlayerProximity()
    {
        // check if bomber has locked onto player
        if (lockOn)
        {
            seePlayer = true;
            attacking = true;
        }
        else
        {
            base.CheckPlayerProximity();
        }
    }

    protected override void FollowPLayerIfSeen()
    {
        if (seePlayer)
        {
            lockOn = true;
        }
        if (lockOn)
        {
            // Chase player
            transform.parent.GetComponent<AIDestinationSetter>().target = player.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            vfx.layer = gameObject.layer;
            vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(vfx, 5f);
            Destroy(transform.parent.gameObject);
        }
    }

    public override string ToString()
    {
        return "EnemyBomber";
    }
}