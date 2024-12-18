using UnityEngine;
using System.Linq;

public class NewBullet : MonoBehaviour
{
    [SerializeField] private float distractionRange = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;

        foreach (NewGuard each in Physics.OverlapSphere(transform.position, distractionRange).Select(each => each.GetComponent<NewGuard>()).Where(each => each != null))
            each.Distract(transform.position);
        Destroy(gameObject);
    }
}
