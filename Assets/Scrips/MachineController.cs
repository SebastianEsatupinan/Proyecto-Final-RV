using UnityEngine;

public class MachineController : MonoBehaviour
{
    public AudioSource machineAudioSource; // AudioSource que representa el sonido continuo de la m�quina

    private bool isMachineOn = false; // Estado de la m�quina

    /// <summary>
    /// Propiedad para obtener el estado actual del panel.
    /// </summary>
    public bool EstadoPanel => isMachineOn;

    /// <summary>
    /// Rutina p�blica para encender la m�quina.
    /// </summary>
    public void EncenderMaquina()
    {
        if (!isMachineOn)
        {
            isMachineOn = true;

            // Activa el AudioSource si est� asignado
            if (machineAudioSource != null)
            {
                machineAudioSource.enabled = true;
                if (!machineAudioSource.isPlaying)
                {
                    machineAudioSource.Play(); // Asegura que el audio comience a reproducirse
                }
            }
            else
            {
                Debug.LogWarning("No se asign� un AudioSource para el sonido de la m�quina.");
            }

            Debug.Log("La m�quina se ha encendido.");
        }
        else
        {
            Debug.Log("La m�quina ya est� encendida.");
        }
    }

    /// <summary>
    /// Rutina p�blica para apagar la m�quina.
    /// </summary>
    public void ApagarMaquina()
    {
        if (isMachineOn)
        {
            isMachineOn = false;

            // Desactiva el AudioSource si est� asignado
            if (machineAudioSource != null)
            {
                machineAudioSource.Stop(); // Detiene el audio
                machineAudioSource.enabled = false; // Desactiva el AudioSource
            }
            else
            {
                Debug.LogWarning("No se asign� un AudioSource para el sonido de la m�quina.");
            }

            Debug.Log("La m�quina se ha apagado.");
        }
        else
        {
            Debug.Log("La m�quina ya est� apagada.");
        }
    }
}
