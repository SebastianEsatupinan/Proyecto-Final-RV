using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPush : MonoBehaviour
{
    public Transform buttonTransform;
    private Vector3 originalPosition;
    private bool isPressed = false;

    [SerializeField] private float pressedZPosition = -0.029f;
    [SerializeField] private float pressSpeed = 5f;
    [SerializeField] private float releaseSpeed = 5f;

    void Start()
    {
        // Guardamos la posición original del botón
        originalPosition = buttonTransform.localPosition;
    }

    void Update()
    {
        if (isPressed)
        {
            // Mueve el botón hacia la posición presionada
            Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y, pressedZPosition);
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, targetPosition, Time.deltaTime * pressSpeed);
        }
        else
        {
            // Si el botón no está presionado, regresa a su posición original suavemente
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, originalPosition, Time.deltaTime * releaseSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el objeto que toca sea la mano
        if (other.CompareTag("Hand")) // Asegúrate de que tu mano tenga el tag "Hand"
        {
            Debug.Log("Se pulso el boton");
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir de la colisión, el botón deja de estar presionado
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Se solto el boton");
            isPressed = false;
        }
    }
}
