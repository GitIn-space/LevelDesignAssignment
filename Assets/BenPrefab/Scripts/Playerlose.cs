using UnityEngine;

public class Playerlose : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnpoint;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = spawnpoint.transform.position;
            gameOverCanvas.SetActive(true);
        }
    
    }
}
