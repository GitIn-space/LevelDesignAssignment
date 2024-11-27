using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float distractionRange = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;

        foreach(Guard each in Physics.OverlapSphere(collision.GetContact(0).point, distractionRange).Select(each => each.GetComponent<Guard>()).Where(each => each != null))
            each.Distract(collision.GetContact(0).point);
        Destroy(gameObject);
    }
}
