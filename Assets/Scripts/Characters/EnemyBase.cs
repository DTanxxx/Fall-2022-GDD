using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Transform firingOrigin = null;
    [SerializeField] protected float fireInterval = 2.0f;
    [SerializeField] protected float playerProximityLimit = 10.0f;
    [SerializeField] protected float speedMultiplier = 1.0f;
    [SerializeField] protected float attackRange = 7.0f;
    [SerializeField] protected int damage = 0;

    protected Rigidbody2D rb;
    protected PlayerMovement player;

    protected bool seePlayer = false;
    protected float fireTimer;
    protected bool attacking = false;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireInterval;
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            return;
        }

        CheckPlayerProximity();
        FacePlayerIfSeen();
        FollowPLayerIfSeen();
        FirePlayerIfSeen();
    }

    protected virtual void CheckPlayerProximity()
    {
        if (player.gameObject.layer == gameObject.layer)
        {
            // same layer, then check distance
            float distance = Vector2.Distance(player.gameObject.transform.position, transform.position);
            if (distance < playerProximityLimit)
            {
                // player in range
                seePlayer = true;
                attacking = true;
                return;
            }
        }
        seePlayer = false;
        attacking = false;
    }

    protected virtual void FacePlayerIfSeen()
    {
        if (seePlayer)
        {
            // Debug.Log("See!");
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 lookDirection = playerPos - rb.position;
            //float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            //rb.rotation = angle;

            if (lookDirection.x < 0)
            {
                // make lookDirection.y negative
                lookDirection = new Vector2(lookDirection.x, -Mathf.Abs(lookDirection.y));
            }
            else
            {
                // make lookDirection.y positive
                lookDirection = new Vector2(lookDirection.x, Mathf.Abs(lookDirection.y));
            }
            Vector3 currScale = transform.localScale;
            float scaler = currScale.y;
            transform.localScale =
                Mathf.Atan2(lookDirection.y, lookDirection.x) > 0 ?
                new Vector3(-1.0f, 1.0f, 1.0f) * scaler : new Vector3(1.0f, 1.0f, 1.0f) * scaler;
        }
    }

    protected virtual void FollowPLayerIfSeen()
    {
        if (seePlayer && Vector2.Distance(player.transform.position, transform.position) > attackRange)
        {
            var player = FindObjectOfType<PlayerMovement>();
            transform.parent.GetComponent<EnemyMovement>().MoveTo(player.transform, speedMultiplier);
        }
    }

    protected virtual void FirePlayerIfSeen()
    {

    }

    public bool GetIsAttacking()
    {
        return attacking;
    }

    public virtual string toString()
    {
        return "Enemy";
    }

}
