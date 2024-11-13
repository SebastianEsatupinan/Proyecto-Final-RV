using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotaci�n de la perilla
    public float minAngle = 0f; // �ngulo m�nimo de rotaci�n en el eje Z
    public float maxAngle = 180f; // �ngulo m�ximo de rotaci�n en el eje Z

    private XRBaseInteractor interactor; // Interactor que agarra la perilla
    private bool isRotating = false; // Estado de rotaci�n
    private float initialInteractorAngle; // �ngulo inicial del interactor
    private float initialKnobAngle; // �ngulo inicial de la perilla
    private Vector3 initialPosition; // Posici�n fija del pivote

    void Start()
    {
        // Configura eventos de agarre y liberaci�n
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        // Guarda la posici�n inicial para fijar el objeto
        initialPosition = rotationPivot.localPosition;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Comienza a rotar cuando se agarra la perilla
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;

        // Guarda el �ngulo inicial del interactor y el �ngulo actual de la perilla en Z
        initialInteractorAngle = CalculateAngle(interactor.transform.position);
        initialKnobAngle = rotationPivot.localEulerAngles.z;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Detiene la rotaci�n cuando se suelta la perilla
        isRotating = false;
        interactor = null;
    }

    void Update()
    {
        if (isRotating && interactor != null)
        {
            RotateKnob();
        }

        // Fija la posici�n en cada frame para evitar movimiento inesperado
        rotationPivot.localPosition = initialPosition;
    }

    private void RotateKnob()
    {
        // Calcula el �ngulo actual del interactor en relaci�n al pivote
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);

        // Calcula el cambio de �ngulo desde el inicio y ajusta con el �ngulo inicial
        float angleDelta = currentInteractorAngle - initialInteractorAngle;
        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica solo la rotaci�n en Z usando Quaternion para bloquear otros ejes
        rotationPivot.localRotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        // Calcula el �ngulo en el plano X-Z entre el interactor y el pivote de rotaci�n
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}

