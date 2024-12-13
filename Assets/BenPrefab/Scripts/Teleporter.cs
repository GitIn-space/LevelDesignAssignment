using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleport;
    public GameObject player;
    public AudioSource stairs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = targetTeleport.transform.position;
            stairs.Play();
        }
           
    }
}
