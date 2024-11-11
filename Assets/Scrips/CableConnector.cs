using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CableConnector : MonoBehaviour
{
    public Transform cableEnd; // Extremo del cable que se mover�
    public Transform connectionPoint; // Punto donde el cable debe conectarse
    public float snapDistance = 0.1f; // Distancia para "pegar" el cable al punto de conexi�n
    public bool isConnected = false; // Indica si el cable est� conectado

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Obtenemos el XRGrabInteractable del cable para detectar cu�ndo se suelta
        grabInteractable = cableEnd.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnCableReleased);
        }
    }

    void Update()
    {
        // Solo se verifica la conexi�n si el cable no est� conectado
        if (!isConnected && Vector3.Distance(cableEnd.position, connectionPoint.position) <= snapDistance)
        {
            ConnectCable();
        }
    }

    private void OnCableReleased(SelectExitEventArgs args)
    {
        // Solo intenta conectar el cable si est� cerca del punto al soltarlo
        if (Vector3.Distance(cableEnd.position, connectionPoint.position) <= snapDistance)
        {
            ConnectCable();
        }
    }

    private void ConnectCable()
    {
        // Fija la posici�n y rotaci�n del extremo del cable en el punto de conexi�n
        cableEnd.position = connectionPoint.position;
        cableEnd.rotation = connectionPoint.rotation;

        // Opcional: Desactiva el XRGrabInteractable para que el cable no se mueva una vez conectado
        grabInteractable.enabled = false;

        // Marcar el cable como conectado
        isConnected = true;

        // Agregar feedback visual o de sonido si es necesario
        Debug.Log("Cable conectado.");
    }
}
