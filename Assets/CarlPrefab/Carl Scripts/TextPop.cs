using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPop : MonoBehaviour
{

    public GameObject FamilyName;

    // Start is called before the first frame update
    void Start()
    {

        FamilyName.SetActive(false);


    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag == "Player")
        {

            FamilyName.SetActive(true);



        }


    }

    private void OnTriggerExit(Collider collision)
    {

        FamilyName.SetActive(false);




    }

}