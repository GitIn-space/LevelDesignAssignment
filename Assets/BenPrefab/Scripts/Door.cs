using UnityEngine;

public class Door : MonoBehaviour
{
    public Key Key;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (Key.isUnlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
}
