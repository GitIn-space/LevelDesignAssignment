using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isUnlocked;
    
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        isUnlocked = true;
    }
}
