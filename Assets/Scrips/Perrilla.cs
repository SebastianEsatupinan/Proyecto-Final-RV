using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotaci�n, normalmente el objeto de la perilla
    public float minAngle = 0f; // �ngulo m�nimo de rotaci�n en el eje Z
    public float maxAngle = 180f; // �ngulo m�ximo de rotaci�n en el eje Z

    private XRBaseInteractor interactor; // Interactor que est� agarrando la perilla
    private bool isRotating = false; // Bandera para saber si estamos rotando
    private float initialInteractorAngle; // �ngulo inicial del interactor respecto al pivote
    private float initialKnobAngle; // �ngulo inicial de la perilla cuando se agarra

    void Start()
    {
        // Configura los eventos de selecci�n para iniciar y detener la rotaci�n
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Inicia la rotaci�n cuando se agarra la perilla
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;

        // Guardamos el �ngulo inicial del interactor respecto al pivote y el �ngulo actual de la perilla
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
    }

    private void RotateKnob()
    {
        // Calcula el �ngulo actual del interactor en relaci�n al pivote
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);

        // Calcula el cambio de �ngulo desde el inicio y ajusta con el �ngulo inicial de la perilla
        float angleDelta = currentInteractorAngle - initialInteractorAngle;
        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica la rotaci�n en el eje Z
        rotationPivot.localEulerAngles = new Vector3(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        // Calcula el �ngulo en el plano X-Z entre el interactor y el pivote de rotaci�n
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}
