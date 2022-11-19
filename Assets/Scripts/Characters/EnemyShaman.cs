using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShaman : EnemyShooterBase{

    List<EnemyShooterBase> enemies = new List<EnemyShooterBase>();
    EnemyShooterBase closest;

    protected override void Start()
    {
        speedMultiplier = 1.50f;
        attackRange = 2.5f;
        fireInterval = 1.0f;
        determineClosest();
        base.Start();
    }

    protected override void Update()
    {
        determineClosest();
        base.Update();
    }

    protected override void FollowPLayerIfSeen()
    
    {   
        /*--Calculates Offset--
        Vector3 distanceVector = transform.position - closest.transform.position ;
        Vector3 distanceVectorNormalized = distanceVector.normalized;
        Vector3 targetPosition = (distanceVectorNormalized * offset);
        Transform closestWithOffset = new GameObject().transform;
        closestWithOffset.transform.position = targetPosition;
        ----------------------*/
        
        /* Follows closest enemy */
        transform.parent.GetComponent<AIDestinationSetter>().target = closest.transform;
    }

    private void determineClosest(){
        foreach(EnemyShooterBase enemy in FindObjectsOfType<EnemyShooterBase>()){
            if (enemy.toString() != "EnemyShaman"){
                enemies.Add(enemy);
            }
        }
        float closestDist = 1000000.0f;
        foreach(EnemyShooterBase enemy in enemies){
            closest = closestDist > Vector3.Distance(enemy.transform.position, transform.position) ? enemy : closest;        
        }
    }

    protected override void FirePlayerIfSeen()
    {
        if (seePlayer && fireTimer <= Mathf.Epsilon)
        {
            // Debug.Log("Fire!");
            var fireDir = (closest.transform.position - transform.position).normalized;
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
        return "EnemyShaman";
    }

}   
