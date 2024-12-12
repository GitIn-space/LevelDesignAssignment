using UnityEngine;

public class GetKey : MonoBehaviour
{
   [SerializeField] GameObject Canvas;
    private void OnCollisionEnter(Collision collision)
    {
        Canvas.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        Canvas.SetActive(false);
    }

}
