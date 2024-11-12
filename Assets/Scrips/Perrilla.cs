using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotación, normalmente el objeto de la perilla
    public float minAngle = 0f; // Ángulo mínimo de rotación en el eje Z
    public float maxAngle = 180f; // Ángulo máximo de rotación en el eje Z

    private XRBaseInteractor interactor; // Interactor que está agarrando la perilla
    private bool isRotating = false; // Bandera para saber si estamos rotando
    private float initialInteractorAngle; // Ángulo inicial del interactor respecto al pivote
    private float initialKnobAngle; // Ángulo inicial de la perilla cuando se agarra

    void Start()
    {
        // Configura los eventos de selección para iniciar y detener la rotación
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Inicia la rotación cuando se agarra la perilla
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;

        // Guardamos el ángulo inicial del interactor respecto al pivote y el ángulo actual de la perilla
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
    }

    private void RotateKnob()
    {
        // Calcula el ángulo actual del interactor en relación al pivote
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);

        // Calcula el cambio de ángulo desde el inicio y ajusta con el ángulo inicial de la perilla
        float angleDelta = currentInteractorAngle - initialInteractorAngle;
        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica la rotación en el eje Z
        rotationPivot.localEulerAngles = new Vector3(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        // Calcula el ángulo en el plano X-Z entre el interactor y el pivote de rotación
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}
