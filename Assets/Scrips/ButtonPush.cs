using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPush : MonoBehaviour
{
    public Transform buttonTransform;
    private Vector3 originalPosition;
    private bool isPressed = false;

    [SerializeField] private float pressedZPosition = -0.029f;
    [SerializeField] private float releaseSpeed = 5f;

    void Start()
    {
        // Guardamos la posición original del botón
        originalPosition = buttonTransform.localPosition;
    }

    void Update()
    {
        // Si el botón no está presionado, regresa a su posición original suavemente
        if (!isPressed)
        {
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, originalPosition, Time.deltaTime * releaseSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el objeto que toca sea la mano
        if (other.CompareTag("Hand")) // Asegúrate de que tu mano tenga el tag "Hand"
        {
            isPressed = true;
            MoveButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir de la colisión, el botón deja de estar presionado
        if (other.CompareTag("Hand"))
        {
            isPressed = false;
        }
    }

    private void MoveButton()
    {
        // Limitamos el movimiento del botón hasta la posición máxima en Z
        Vector3 newPosition = buttonTransform.localPosition;
        newPosition.z = Mathf.Max(pressedZPosition, newPosition.z);
        buttonTransform.localPosition = newPosition;
    }
}
