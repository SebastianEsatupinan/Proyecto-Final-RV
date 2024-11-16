using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TeletransportarXR : MonoBehaviour
{
    public Transform cubo1; // Asigna el transform de Cubo1 en el Inspector
    public Transform cubo2; // Asigna el transform de Cubo2 en el Inspector
    private Rigidbody rb;   // Referencia al Rigidbody del XR Rig

    void Start()
    {
        // Obtiene el Rigidbody del XR Rig
        rb = GetComponent<Rigidbody>();

        // Asegúrate de que está en modo Kinematic para prevenir problemas de física
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprueba si el XR Rig entra en Cubo1
        if (other.gameObject.transform == cubo1)
        {
            // Inicia la rutina de teletransporte para mayor estabilidad
            StartCoroutine(Teletransportar());
        }
    }

    IEnumerator Teletransportar()
    {
        // Desactiva temporalmente el Rigidbody para evitar movimiento indeseado
        rb.isKinematic = true;

        // Teletransporta el XR Rig a la posición de Cubo2
        transform.position = cubo2.position;

        // Reinicia la velocidad y rotación del Rigidbody para evitar que "caiga"
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Espera un fotograma para asegurar estabilidad en la nueva posición
        yield return null;

        // Reactiva el Rigidbody en modo Kinematic
        rb.isKinematic = true;
    }
}

    


