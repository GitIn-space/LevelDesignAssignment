using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public Key Key;

    [SerializeField] AudioSource AudioSource;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (Key.isUnlocked == true)
        {
            gameObject.SetActive(false);
            AudioSource.Play();
        }
    }
}
