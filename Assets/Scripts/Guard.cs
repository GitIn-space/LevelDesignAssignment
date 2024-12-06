using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float speed = 10;
    [SerializeField] private float minDist = 0.5f;
    [SerializeField] private float distractDuration = 3f;
    [SerializeField] private float distractLongReset = 10;

    private int index = 0;
    private int indexDir = 1;
    private Rigidbody body;
    private bool distracted = false;
    private Coroutine distractRoutine;
    private Coroutine distractLongRoutine;

    private void Awake()
    {
        if (patrolPoints.Count == 0)
        {
            GameObject go = new GameObject();
            go.transform.position = transform.position;
            patrolPoints.Add(go.transform);
        }

        body = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dist = new Vector3(patrolPoints[index].position.x, 0, patrolPoints[index].position.z) - new Vector3(transform.position.x, 0, transform.position.z);

        if (dist.magnitude < minDist && patrolPoints.Count <= 1)
            return;

        transform.rotation = Quaternion.LookRotation(dist, Vector3.up);
        body.linearVelocity = dist.normalized * speed;

        if (dist.magnitude > minDist)
            return;

        if (dist.magnitude <= minDist && distracted)
        {
            body.linearVelocity = Vector3.zero;

            if (distractRoutine == null)
                distractRoutine = StartCoroutine(Distracted());

            return;
        }

        if (((index += indexDir) >= patrolPoints.Count))
        {
            index = patrolPoints.Count - 1;
            indexDir *= -1;
        }
        else if (index < 0)
        {
            index = 0;
            indexDir *= -1;
        }
    }

    public void Distract(Vector3 point)
    {
        if (!distracted)
        {
            GameObject go = new GameObject();
            go.transform.position = point;
            patrolPoints.Insert(++index, go.transform);
            distracted = true;
            distractLongRoutine = StartCoroutine(DistractFailSafe());
        }
    }

    private IEnumerator Distracted()
    {
        yield return new WaitForSeconds(distractDuration);
        StopDistracted();
    }

    private IEnumerator DistractFailSafe()
    {
        yield return new WaitForSeconds(distractLongReset);
        if (distractRoutine != null)
            StopDistracted();
    }

    private void StopDistracted()
    {
        distracted = false;
        patrolPoints.RemoveAt(index);
        index--;
        StopCoroutine(distractRoutine);
        StopCoroutine(distractLongRoutine);
    }
}