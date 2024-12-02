using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerBridgeConptroller : MonoBehaviour
{
    [SerializeField] private Animator myBridge = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myBridge.Play("DrawBridgeOpen", 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                myBridge.Play("DrawBridgeClose", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
