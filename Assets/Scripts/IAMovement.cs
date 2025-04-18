using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.AI;

public class IAMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float visionRange = 5f;

    private Transform currentTarget;
    private bool chasingPlayer = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB;
    }


    // Update is called once per frame
    public void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= visionRange)
        {
            chasingPlayer = true;
        }
        else if (chasingPlayer && distanceToPlayer > visionRange)
        {
            chasingPlayer = false;
            currentTarget = GetNextPatrolPoint();
        }
    }

    public void FixedUpdate()
    {
        if (chasingPlayer)
        {
            MoveTowards(player.position);
        }
        else
        {
            MoveTowards(currentTarget.position);

            // Change destiny if the enemy arrives to the target position
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.2f)
            {
                currentTarget = GetNextPatrolPoint();
            }
        }
    }

    public void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private Transform GetNextPatrolPoint()
    {
        return currentTarget == pointA ? pointB : pointA;
    }
}
