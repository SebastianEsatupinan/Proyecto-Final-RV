using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DialAudioController : MonoBehaviour
{
    [Header("Configuración del dial")]
    public XRKnob dialKnob; // Asocia el XRKnob
    public AudioSource audioSource; // El AudioSource que será modificado

    [Header("Configuración de rangos de ángulos")]
    [Tooltip("Ángulo mínimo del rango (verde - volumen bajo)")]
    public float minAngle = -90.0f; // Ángulo mínimo del dial
    [Tooltip("Ángulo máximo del rango (rojo - volumen alto)")]
    public float maxAngle = 90.0f; // Ángulo máximo del dial

    [Header("Configuración de volumen")]
    [Tooltip("Volumen mínimo (en el ángulo mínimo)")]
    public float minVolume = 0.1f; // Volumen más bajo
    [Tooltip("Volumen máximo (en el ángulo máximo)")]
    public float maxVolume = 1.0f; // Volumen más alto

    private void Update()
    {
        if (dialKnob != null && audioSource != null)
        {
            // Convierte el valor del dial (0-1) en un ángulo
            float knobAngle = Mathf.Lerp(minAngle, maxAngle, dialKnob.value);

            // Ajusta el volumen según el ángulo del dial
            float newVolume = Mathf.Lerp(minVolume, maxVolume, (knobAngle - minAngle) / (maxAngle - minAngle));
            audioSource.volume = newVolume;

            // (Opcional) Debug para ver los valores en tiempo de ejecución
            Debug.Log($"Ángulo del dial: {knobAngle}, Volumen ajustado: {newVolume}");
        }
    }
}


