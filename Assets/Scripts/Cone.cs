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
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.Raycast(guardComp.transform.position, (other.transform.position - guardComp.transform.position), out RaycastHit hit, 100f, ~LayerMask.GetMask("Cone"));
            if (hit.collider.gameObject.CompareTag("Player"))
                guardComp.Detect(other.transform);
        }
    }
}
