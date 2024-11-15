using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotaci�n de la perilla
    public float minAngle = 0f; // �ngulo m�nimo de rotaci�n en el eje Z
    public float maxAngle = 180f; // �ngulo m�ximo de rotaci�n en el eje Z
    public bool allowClockwise = true; // Determina si el giro es en sentido horario

    private XRBaseInteractor interactor; // Interactor que agarra la perilla
    private bool isRotating = false; // Estado de rotaci�n
    private float initialInteractorAngle; // �ngulo inicial del interactor
    private float initialKnobAngle; // �ngulo inicial de la perilla
    private Vector3 initialPosition; // Posici�n fija del pivote

    void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
        initialPosition = rotationPivot.localPosition;
    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;
        initialInteractorAngle = CalculateAngle(interactor.transform.position);
        initialKnobAngle = rotationPivot.localEulerAngles.z;
    }

    public void OnReleased(SelectExitEventArgs args)
    {
        isRotating = false;
        interactor = null;
    }

    void Update()
    {
        if (isRotating && interactor != null)
        {
            RotateKnob();
        }
        rotationPivot.localPosition = initialPosition;
    }

    private void RotateKnob()
    {
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);
        float angleDelta = currentInteractorAngle - initialInteractorAngle;

        // Ajustar el �ngulo permitido seg�n la direcci�n permitida
        if (!allowClockwise && angleDelta > 0)
            angleDelta = 0; // Bloquea giro horario
        if (allowClockwise && angleDelta < 0)
            angleDelta = 0; // Bloquea giro antihorario

        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica solo la rotaci�n en Z y bloquea X e Y
        rotationPivot.localRotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}
