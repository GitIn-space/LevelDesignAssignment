using UnityEngine;

public class Cone : MonoBehaviour
{
    private Guard guardComp;

    private void Awake()
    {
        guardComp = GetComponentInParent<Guard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            if(Physics.Raycast(transform.position, Vector3.Normalize(other.transform.position - transform.position), LayerMask.GetMask("Player")))
                guardComp.Detect(other.transform);
    }
}
