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
    private float startingAngle; // Ángulo inicial cuando se agarra la perilla

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
        interactor = args.interactor;
        isRotating = true;
        startingAngle = GetLocalRotationAngle();
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
        // Calcula la dirección del controlador en relación al pivote
        Vector3 direction = interactor.transform.position - rotationPivot.position;

        // Calcula el ángulo en el plano X-Z para asegurar que solo rota en el eje Z
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Calcula el nuevo ángulo restringido entre minAngle y maxAngle
        float newAngle = Mathf.Clamp(startingAngle + angle, minAngle, maxAngle);

        // Aplica la rotación solo en el eje Z, manteniendo X e Y en 0
        rotationPivot.localEulerAngles = new Vector3(0f, 0f, newAngle);
    }

    private float GetLocalRotationAngle()
    {
        // Retorna solo el ángulo de rotación en el eje Z
        return rotationPivot.localEulerAngles.z;
    }
}
