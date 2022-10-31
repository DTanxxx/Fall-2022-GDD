using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float arrivalOffsetAllowed = 0.25f;
    [SerializeField] private float pauseTime = 2f;

    private GameObject nextWaypoint;
    private Rigidbody2D rb;
    private EnemyBase enemy;

    private int currentWaypointIndex;
    private bool startingMoving = true;
    private Vector3 faceDir;

    private void Start()
    {
        FindObjectOfType<GameManager>().RegisterEnemy();

        // Initialize enemy location and variables
        int randomIndex = Random.Range(0, waypoints.Length);
        currentWaypointIndex = randomIndex;
        GameObject initialWaypoint = waypoints[randomIndex];
        transform.position = initialWaypoint.transform.position;
        nextWaypoint = initialWaypoint;
        rb = GetComponentInChildren<Rigidbody2D>();
        enemy = GetComponentInChildren<EnemyBase>();
    }

    private void Update()
    {
        if (enemy.GetIsAttacking()) { return; }
        float distToNextWaypoint =
            Vector2.Distance(transform.position, nextWaypoint.transform.position);
        if (distToNextWaypoint <= arrivalOffsetAllowed)
        {
            startingMoving = false;
            // Arrive at nextWaypoint, pick a new waypoint and start going there
            int newIndex = Random.Range(0, waypoints.Length);
            while (newIndex == currentWaypointIndex)
            {
                newIndex = Random.Range(0, waypoints.Length);
            }
            currentWaypointIndex = newIndex;
            nextWaypoint = waypoints[currentWaypointIndex];
            StartCoroutine(PauseThenStart());
        }
        else if (startingMoving)
        {
            // move to next waypoint
            MoveTo(nextWaypoint.transform, 1f);
        }

        faceDir = (nextWaypoint.transform.position - transform.position).normalized;
        if (enemy.toString() == "EnemyMeleeBase")
        {
            if (faceDir.x < 0)
            {
                // make lookDirection.y negative
                faceDir = new Vector2(faceDir.x, -Mathf.Abs(faceDir.y));
            }
            else
            {
                // make lookDirection.y positive
                faceDir = new Vector2(faceDir.x, Mathf.Abs(faceDir.y));
            }
            Vector3 currScale = transform.localScale;
            float scaler = currScale.y;
            rb.transform.localScale =
                Mathf.Atan2(faceDir.y, faceDir.x) > 0 ? 
                new Vector3(-1.0f, 1.0f, 1.0f) * scaler : new Vector3(1.0f, 1.0f, 1.0f) * scaler;
        }
        else
        {
            //float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg - 90f;
            //rb.rotation = angle;
        }
    }

    public void MoveTo(Transform position, float multiplier)
    {
        GetComponent<AIDestinationSetter>().target = position;
        GetComponent<AIPath>().maxSpeed = patrolSpeed * multiplier;
       /* transform.position = Vector2.MoveTowards(
                transform.position,
                position,
                Time.deltaTime * patrolSpeed * multiplier);*/
    }

    private IEnumerator PauseThenStart()
    {
        yield return new WaitForSeconds(pauseTime);
        startingMoving = true;
    }
}