using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public Key Key;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] GameObject canvas;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (Key.isUnlocked == true)
        {
            gameObject.SetActive(false);
            AudioSource.Play();
            canvas.SetActive(false);
            
        }
    }
}
