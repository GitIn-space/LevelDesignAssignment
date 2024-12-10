using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleport;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        player.transform.position = targetTeleport.transform.position;
    }
}
