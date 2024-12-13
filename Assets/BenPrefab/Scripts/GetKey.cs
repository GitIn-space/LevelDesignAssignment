using UnityEngine;

public class GetKey : MonoBehaviour
{
   [SerializeField] GameObject Canvas;
    private void OnCollisionEnter(Collision collision)
    {
        Canvas.SetActive(true);
    }

  

}
