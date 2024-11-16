using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Llave"))
        {
            Destroy(other.gameObject);
        }



    }
}
