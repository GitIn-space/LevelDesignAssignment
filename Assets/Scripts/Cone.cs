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
            guardComp.Detect(other.transform);
    }
}
