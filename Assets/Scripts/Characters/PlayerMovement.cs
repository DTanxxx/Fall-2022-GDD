using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed = 7f;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Camera cam;
    private Vector2 movement;
    private Vector2 mousePos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    public void SpeedUp(float amount)
    {
        moveSpeed = Mathf.Min(moveSpeed + amount, maxMoveSpeed);
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // movement is controlled by key press
        // moveSpeed is set in inspector
        transform.position = rb.position + movement * moveSpeed * Time.deltaTime;
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
