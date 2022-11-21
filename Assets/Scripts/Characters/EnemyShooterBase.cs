using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public abstract class EnemyShooterBase : EnemyBase{
    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected float bulletForce = 20f;
    protected int timeUntilNextDodge = 0;

    private bool timerRunning = false;

    protected override void Start()
    {
        base.Start();
        damage = 1;
    }

    protected override void Update()
    {
        base.Update();
        DodgePlayerIfSeen();
    }

    protected override void FirePlayerIfSeen()
    {
        if (seePlayer && fireTimer <= Mathf.Epsilon)
        {
            Debug.Log("Fire!");
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

    protected void DodgePlayerIfSeen()
    {
        if (seePlayer && !timerRunning)
        {
            //timeUntilNextDodge = Random.Next(2, 10);
            timeUntilNextDodge = 5;
            timerRunning = true;
        } 
        else 
        {
            while (timeUntilNextDodge > 0)
            {
                Thread.Sleep(1000);
                timeUntilNextDodge -= 1;
            }
            Debug.Log("Dodge");
            timerRunning = false;
        }
    }

    public override string toString(){
        return "EnemyShooterBase";
    }
}   

