using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBase : EnemyBase
{
    protected Animator m_animator;
    private float attackRadius;
    private Vector3 prevPosition;


    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
        base.Start();
        fireInterval = 2.0f;
        damage = 2;
        prevPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FollowPLayerIfSeen()
    {
        base.FollowPLayerIfSeen();
        m_animator.SetInteger("AnimState", 2);
    }

    protected override void FirePlayerIfSeen()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (attacking && fireTimer <= Mathf.Epsilon && distance <= attackRange)
        {
            m_animator.SetTrigger("Attack");
            fireTimer = fireInterval;
        }
        else if (attacking && fireTimer > Mathf.Epsilon && distance <= attackRange)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
            m_animator.SetInteger("AnimState", 1);
        }
    }

    // Called by animation events
    public void ApplyMeleeDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(firingOrigin.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<PlayerMovement>())
            {
                //Debug.Log("Melee Hit!");
                player.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected(){
        if(firingOrigin == null) return;
        Gizmos.DrawWireSphere(firingOrigin.position, attackRadius);
    }

    public override string toString()
    {
        return "EnemyMeleeBase";
    }
}
