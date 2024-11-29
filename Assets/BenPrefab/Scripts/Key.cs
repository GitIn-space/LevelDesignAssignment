using UnityEngine;

public class Key
{
    public bool isUnlocked;
    [SerializeField] GameObject gameObject;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        isUnlocked = true;
    }
}
