using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShooterBase : EnemyBase{
    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected float bulletForce = 20f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FirePlayerIfSeen()
    {
        if (seePlayer && fireTimer <= Mathf.Epsilon)
        {
            // Debug.Log("Fire!");
            var fireDir = (player.transform.position - transform.position).normalized;
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg - 90f;
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(fireDir * bulletForce, ForceMode2D.Impulse);
            rb.rotation = angle;
            //rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
            fireTimer = fireInterval;
        }
        else if (seePlayer && fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }

    public override string toString(){
        return "EnemyShooterBase";
    }
}   

