using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRApplyHandTextureOnGrab : MonoBehaviour
{
    [Header("Manos del jugador")]
    public GameObject leftHand; // Modelo de la mano izquierda
    public GameObject rightHand; // Modelo de la mano derecha

    [Header("Material de las manos")]
    public Material newHandMaterial; // Material que se aplicará a las manos

    private void OnEnable()
    {
        // Configura el evento del XRGrabInteractable
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnDisable()
    {
        // Elimina el evento para evitar problemas de memoria
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Verifica si el objeto tiene el tag "guante"
        if (CompareTag("guante"))
        {
            // Cambia el material de las manos
            ApplyMaterialToHands();

            // Destruye el objeto del mundo
            Destroy(gameObject);

            Debug.Log("El material del guante fue aplicado a las manos y el objeto fue eliminado.");
        }
        else
        {
            Debug.LogWarning("El objeto agarrado no tiene el tag 'guante'.");
        }
    }

    private void ApplyMaterialToHands()
    {
        if (newHandMaterial == null)
        {
            Debug.LogError("No se asignó un material para las manos. Por favor, asigna un material en el Inspector.");
            return;
        }

        // Cambia el material de la mano izquierda
        if (leftHand != null)
        {
            Renderer leftRenderer = leftHand.GetComponent<Renderer>();
            if (leftRenderer != null)
            {
                leftRenderer.material = newHandMaterial;
            }
            else
            {
                Debug.LogWarning("La mano izquierda no tiene un componente Renderer.");
            }
        }
        else
        {
            Debug.LogWarning("No se asignó un modelo para la mano izquierda.");
        }

        // Cambia el material de la mano derecha
        if (rightHand != null)
        {
            Renderer rightRenderer = rightHand.GetComponent<Renderer>();
            if (rightRenderer != null)
            {
                rightRenderer.material = newHandMaterial;
            }
            else
            {
                Debug.LogWarning("La mano derecha no tiene un componente Renderer.");
            }
        }
        else
        {
            Debug.LogWarning("No se asignó un modelo para la mano derecha.");
        }
    }
}

