using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public Key Key;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject door;
 
    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        if (Key.isUnlocked == true)
        {
            door.SetActive(false);
            AudioSource.Play();
           
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        canvas.SetActive(false);
    }
   
}
