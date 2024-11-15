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
        // Guardamos la posici�n original del bot�n
        originalPosition = buttonTransform.localPosition;
    }

    void Update()
    {
        // Si el bot�n no est� presionado, regresa a su posici�n original suavemente
        if (!isPressed)
        {
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, originalPosition, Time.deltaTime * releaseSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el objeto que toca sea la mano
        if (other.CompareTag("Hand")) // Aseg�rate de que tu mano tenga el tag "Hand"
        {
            isPressed = true;
            MoveButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir de la colisi�n, el bot�n deja de estar presionado
        if (other.CompareTag("Hand"))
        {
            isPressed = false;
        }
    }

    private void MoveButton()
    {
        // Limitamos el movimiento del bot�n hasta la posici�n m�xima en Z
        Vector3 newPosition = buttonTransform.localPosition;
        newPosition.z = Mathf.Max(pressedZPosition, newPosition.z);
        buttonTransform.localPosition = newPosition;
    }
}
