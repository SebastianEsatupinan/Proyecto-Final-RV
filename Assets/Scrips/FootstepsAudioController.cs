using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class FootstepsAudioController : MonoBehaviour
{
    public AudioSource footstepsAudioSource; // Asigna aquí el AudioSource (soundsteps-248147)
    public Transform mainCameraTransform; // Asigna aquí el Transform de la Main Camera
    private Vector3 lastPosition; // Variable para almacenar la última posición de la Main Camera

    void Start()
    {
        if (footstepsAudioSource == null)
        {
            Debug.LogError("No se ha asignado el AudioSource de pasos.");
        }

        if (mainCameraTransform == null)
        {
            Debug.LogError("No se ha asignado el Transform de la Main Camera.");
        }

        // Inicializamos la última posición
        lastPosition = mainCameraTransform.position;
    }

    void Update()
    {
        // Comprobamos el cambio en X y Z
        Vector3 currentPosition = mainCameraTransform.position;
        float deltaX = Mathf.Abs(currentPosition.x - lastPosition.x);
        float deltaZ = Mathf.Abs(currentPosition.z - lastPosition.z);

        // Si hay movimiento en X o Z
        if (deltaX > 0.01f || deltaZ > 0.01f)
        {
            // Si el sonido no está reproduciéndose, lo iniciamos
            if (!footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Play();
            }
        }
        else
        {
            // Si no hay movimiento, detenemos el sonido
            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
        }

        // Actualizamos la última posición
        lastPosition = currentPosition;
    }
}
