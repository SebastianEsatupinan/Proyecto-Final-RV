using System.Collections;
using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    // Referencia al AudioSource que se activar�
    public AudioSource audioSource;
    // Etiqueta a detectar
    public string targetTag = "hands"; // Configurable desde el Inspector
    private bool canPlaySound = true; // Controla si el sonido puede reproducirse

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene la etiqueta correcta
        if (other.CompareTag(targetTag) && canPlaySound)
        {
            PlaySound();
            canPlaySound = false; // Deshabilita la reproducci�n hasta que salga
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale tiene la etiqueta correcta
        if (other.CompareTag(targetTag))
        {
            canPlaySound = true; // Habilita la reproducci�n nuevamente
        }
    }

    private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado ning�n AudioSource.");
        }
    }
}
