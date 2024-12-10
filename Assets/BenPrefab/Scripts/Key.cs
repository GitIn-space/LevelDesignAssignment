using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isUnlocked;
    public AudioSource keyget;
    
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        keyget.Play();
        isUnlocked = true;
    }
}
