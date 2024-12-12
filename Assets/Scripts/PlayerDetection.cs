using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    public Transform player; // Assign the Player Transform in the Inspector
    public float detectionRadius = 10f; // Distance at which the guard detects the player
    public float fieldOfView = 45f; // Guard's field of view in degrees
    public float loseSightTime = 3f; // Time before the guard returns to patrolling after losing sight

    private NavMeshAgent navMeshAgent;
    private GuardPatrol guardPatrol;
    private bool isChasing = false;
    private float timeSinceLastSeen = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        guardPatrol = GetComponent<GuardPatrol>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within detection radius
        if (distanceToPlayer <= detectionRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            // Check if player is within the field of view
            if (angleToPlayer <= fieldOfView / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out hit, detectionRadius))
                {
                    // If the raycast hits the player, start chasing
                    if (hit.transform.CompareTag("Player"))
                    {
                        StartChasing();
                        return;
                    }
                }
            }
        }

        // If player is out of sight, start counting to return to patrolling
        if (isChasing)
        {
            timeSinceLastSeen += Time.deltaTime;
            if (timeSinceLastSeen >= loseSightTime)
            {
                StopChasing();
            }
        }
    }

    void StartChasing()
    {
        isChasing = true;
        timeSinceLastSeen = 0f; // Reset the timer
        guardPatrol.enabled = false; // Stop patrolling
        navMeshAgent.SetDestination(player.position); // Move towards the player
    }

    void StopChasing()
    {
        isChasing = false;
        guardPatrol.enabled = true; // Resume patrolling
        guardPatrol.ResetPatrol(); // Reset to the nearest patrol point
    }
}
