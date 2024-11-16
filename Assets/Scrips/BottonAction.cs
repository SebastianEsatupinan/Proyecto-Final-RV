using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Importa UnityEngine.Events para usar UnityEvent

public class ButtonAction : MonoBehaviour
{
    public UnityEvent onButtonPressed; // Evento personalizado para asignar en el inspector
    public Vector3 pressedOffset = new Vector3(0, 0, -0.05f); // Movimiento hacia atr�s al presionar
    public float pressSpeed = 5f; // Velocidad de movimiento
    private Vector3 originalPosition;
    private bool isButtonPressed = false;
    private bool isHandInside = false;

    private void Start()
    {
        // Guarda la posici�n original del bot�n
        originalPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene la etiqueta "hands"
        if (other.CompareTag("hands"))
        {
            if (!isButtonPressed)
            {
                isButtonPressed = true;
                isHandInside = true;

                // Ejecuta el UnityEvent asignado en el inspector
                onButtonPressed.Invoke();

                // Inicia la animaci�n de presionado
                StopAllCoroutines();
                StartCoroutine(MoveButton(originalPosition + pressedOffset)); // Mueve hacia atr�s
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale tiene la etiqueta "hands"
        if (other.CompareTag("hands"))
        {
            isButtonPressed = false;
            isHandInside = false;

            // Inicia la animaci�n para regresar a la posici�n original
            StopAllCoroutines();
            StartCoroutine(MoveButton(originalPosition)); // Vuelve a la posici�n original
        }
    }

    private IEnumerator MoveButton(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, pressSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = targetPosition; // Asegura que llegue exactamente a la posici�n final
    }
}
