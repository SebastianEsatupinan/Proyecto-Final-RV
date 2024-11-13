using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotación de la perilla
    public float minAngle = 0f; // Ángulo mínimo de rotación en el eje Z
    public float maxAngle = 180f; // Ángulo máximo de rotación en el eje Z

    private XRBaseInteractor interactor; // Interactor que agarra la perilla
    private bool isRotating = false; // Estado de rotación
    private float initialInteractorAngle; // Ángulo inicial del interactor
    private float initialKnobAngle; // Ángulo inicial de la perilla
    private Vector3 initialPosition; // Posición fija del pivote

    void Start()
    {
        // Configura eventos de agarre y liberación
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        // Guarda la posición inicial para fijar el objeto
        initialPosition = rotationPivot.localPosition;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Comienza a rotar cuando se agarra la perilla
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;

        // Guarda el ángulo inicial del interactor y el ángulo actual de la perilla en Z
        initialInteractorAngle = CalculateAngle(interactor.transform.position);
        initialKnobAngle = rotationPivot.localEulerAngles.z;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Detiene la rotación cuando se suelta la perilla
        isRotating = false;
        interactor = null;
    }

    void Update()
    {
        if (isRotating && interactor != null)
        {
            RotateKnob();
        }

        // Fija la posición en cada frame para evitar movimiento inesperado
        rotationPivot.localPosition = initialPosition;
    }

    private void RotateKnob()
    {
        // Calcula el ángulo actual del interactor en relación al pivote
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);

        // Calcula el cambio de ángulo desde el inicio y ajusta con el ángulo inicial
        float angleDelta = currentInteractorAngle - initialInteractorAngle;
        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica solo la rotación en Z usando Quaternion para bloquear otros ejes
        rotationPivot.localRotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        // Calcula el ángulo en el plano X-Z entre el interactor y el pivote de rotación
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}

