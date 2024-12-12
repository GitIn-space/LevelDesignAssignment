using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array of patrol waypoints
    public float waitTimeAtWaypoint = 2f; // Time to wait at each waypoint
    public NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent

    private int currentWaypointIndex = 0; // Current waypoint index
    private bool isWaiting = false; // Whether the guard is waiting at a waypoint
    private float waitTimer = 0f;

    void Start()
    {
        if (waypoints.Length > 0 && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        else
        {
            Debug.LogError("Waypoints or NavMeshAgent not assigned!");
        }
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtWaypoint)
            {
                isWaiting = false;
                MoveToNextWaypoint();
            }
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            isWaiting = true;
            waitTimer = 0f;
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    // Reset the patrol when called
    public void ResetPatrol()
    {
        currentWaypointIndex = FindClosestWaypointIndex(); // Find the nearest waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position); // Resume patrol
    }

    // Find the closest waypoint to the guard's current position
    private int FindClosestWaypointIndex()
    {
        float shortestDistance = Mathf.Infinity;
        int closestIndex = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    // Get the position of the closest waypoint (used by PlayerDetection if needed)
    public Vector3 GetClosestWaypointPosition()
    {
        return waypoints[FindClosestWaypointIndex()].position;
    }
}
