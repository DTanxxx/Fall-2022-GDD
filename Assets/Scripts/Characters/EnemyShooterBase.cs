using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Random = System.Random;

public abstract class EnemyShooterBase : EnemyBase{
    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected float bulletForce = 20f;
    [SerializeField] protected GameObject strafeWaypoint;
    protected float timeUntilNextDodge = 1f;
    [SerializeField] protected float dodgeCooldown = 1f; //cool down for strafing
    protected bool dodgeLeft = true;
    protected bool isHorizontalDodge = true;
    protected Vector3 offset;

    private bool timerRunning = false;
    private static Random rnd = new Random();

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
            ////Debug.Log("Fire!");
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


    /* If player is seen, enemy starts strafing in all 4 directions */
    protected void DodgePlayerIfSeen()
    {
        if (seePlayer)
        {
            timeUntilNextDodge -= Time.deltaTime;
            if (timeUntilNextDodge <= 0f)
            {
                //Debug.Log("Dodge");
                Dodge();
                timeUntilNextDodge = dodgeCooldown;
            }
        }
        else
        {
            timeUntilNextDodge = dodgeCooldown;
        }
    }

    /* Strafes on both x and y axis */
    protected void Dodge()
    {
        float offsetAmount = (float) rnd.NextDouble();
        if (dodgeLeft == true) {
            offsetAmount = -offsetAmount;
            dodgeLeft = false;
        } else {
            dodgeLeft = true;
        }
        offset = orientation(offsetAmount, rnd.Next(2));
        Vector3 newPos = transform.position + offset;
        Vector3 localPos = transform.InverseTransformPoint(newPos);
        strafeWaypoint.transform.position = newPos;
        transform.parent.GetComponent<EnemyMovement>().MoveTo(strafeWaypoint.transform, speedMultiplier * 6);
    }

    /* Choosing to strafe on x or y axis */
    private Vector3 orientation(float offset, int isHorizontal)
    {
        if (isHorizontal == 0) {
            return new Vector3(offset, 0, 0);
        } else {
            return new Vector3(0, offset, 0);
        }
    }


    public override string toString(){
        return "EnemyShooterBase";
    }
}   

