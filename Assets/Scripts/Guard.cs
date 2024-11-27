using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float speed = 0;
    [SerializeField] private float minDist = 0.5f;

    private int index = 0;
    private Rigidbody2D body;

    private void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        
        if(patrolPoints.Count > 0)
            body.linearVelocity = (patrolPoints[index].position - transform.position).normalized * speed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dist = patrolPoints[index].position - transform.position;

        if (dist.magnitude > minDist)
            return;
        else if (++index >= patrolPoints.Count)
            index = 0;

        dist = patrolPoints[index].position - transform.position;
        body.linearVelocity = dist.normalized * speed;
    }
}