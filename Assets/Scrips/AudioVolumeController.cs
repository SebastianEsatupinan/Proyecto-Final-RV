using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DialAudioController : MonoBehaviour
{
    [Header("Configuraci�n del dial")]
    public XRKnob dialKnob; // Asocia el XRKnob
    public AudioSource audioSource; // El AudioSource que ser� modificado

    [Header("Configuraci�n de rangos de �ngulos")]
    [Tooltip("�ngulo m�nimo del rango (verde - volumen bajo)")]
    public float minAngle = -90.0f; // �ngulo m�nimo del dial
    [Tooltip("�ngulo m�ximo del rango (rojo - volumen alto)")]
    public float maxAngle = 90.0f; // �ngulo m�ximo del dial

    [Header("Configuraci�n de volumen")]
    [Tooltip("Volumen m�nimo (en el �ngulo m�nimo)")]
    public float minVolume = 0.1f; // Volumen m�s bajo
    [Tooltip("Volumen m�ximo (en el �ngulo m�ximo)")]
    public float maxVolume = 1.0f; // Volumen m�s alto

    private void Update()
    {
        if (dialKnob != null && audioSource != null)
        {
            // Convierte el valor del dial (0-1) en un �ngulo
            float knobAngle = Mathf.Lerp(minAngle, maxAngle, dialKnob.value);

            // Ajusta el volumen seg�n el �ngulo del dial
            float newVolume = Mathf.Lerp(minVolume, maxVolume, (knobAngle - minAngle) / (maxAngle - minAngle));
            audioSource.volume = newVolume;

            // (Opcional) Debug para ver los valores en tiempo de ejecuci�n
            Debug.Log($"�ngulo del dial: {knobAngle}, Volumen ajustado: {newVolume}");
        }
    }
}


