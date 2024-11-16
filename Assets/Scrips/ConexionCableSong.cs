using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CableConectadoSong : MonoBehaviour
{
    // Referencia al XR Socket Interactor
    private XRSocketInteractor socketInteractor;

    // Referencia al clip de audio
    public AudioClip conexionCableSong;

    // Referencia al AudioSource
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente XR Socket Interactor
        socketInteractor = GetComponent<XRSocketInteractor>();

        if (socketInteractor == null)
        {
            Debug.LogError("El XRSocketInteractor no está configurado en el objeto.");
            return;
        }

        // Verificar si el AudioSource ya está presente; si no, agregar uno
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configurar el clip de audio en el AudioSource
        audioSource.clip = conexionCableSong;

        // Suscribirse al evento Select Entered
        socketInteractor.selectEntered.AddListener(OnCableConnected);
    }

    // Método que se ejecuta cuando un objeto es colocado en el socket
    private void OnCableConnected(SelectEnterEventArgs args)
    {
        if (audioSource != null && conexionCableSong != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("El AudioSource o el clip de audio no están configurados.");
        }
    }

    private void OnDestroy()
    {
        // Asegurarse de desuscribirse del evento al destruirse el objeto
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnCableConnected);
        }
    }
}
