using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShaman : EnemyShooterBase{

    List<EnemyBase> enemies = new List<EnemyBase>();
    EnemyBase closest;

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
        enemies.Clear();
        foreach(EnemyBase enemy in FindObjectsOfType<EnemyBase>()){
            if (enemy.toString() != "EnemyShaman"){
                enemies.Add(enemy);
            }
        }
        float closestDist = 1000000.0f;
        foreach(EnemyBase enemy in enemies){
            var dist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist < closestDist)
            {
                closest = enemy;
                closestDist = dist;
            }
        }
    }

    protected override void FirePlayerIfSeen()
    {
        if (fireTimer <= Mathf.Epsilon)
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
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }



    public override string toString(){
        return "EnemyShaman";
    }

}   
