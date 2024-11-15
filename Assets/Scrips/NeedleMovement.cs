using System.Collections;
using UnityEngine;

public class NeedleMovement : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.forward; // Eje de rotaci�n (por ejemplo, Vector3.forward para el eje Z)
    public float minRotation = -45f; // �ngulo m�nimo relativo a la rotaci�n actual
    public float maxRotation = 45f; // �ngulo m�ximo relativo a la rotaci�n actual
    public float speed = 2f; // Velocidad de la animaci�n

    private bool isMoving = false; // Controla si la aguja est� en movimiento
    private Quaternion originalRotation; // Rotaci�n inicial del objeto
    private Quaternion minRotationQuaternion; // Rotaci�n m�nima como Quaternion
    private Quaternion maxRotationQuaternion; // Rotaci�n m�xima como Quaternion

    private void Start()
    {
        // Guarda la rotaci�n inicial del objeto
        originalRotation = transform.localRotation;

        // Calcula las rotaciones m�nima y m�xima en base a la rotaci�n actual del objeto
        minRotationQuaternion = originalRotation * Quaternion.AngleAxis(minRotation, rotationAxis);
        maxRotationQuaternion = originalRotation * Quaternion.AngleAxis(maxRotation, rotationAxis);

        // Establece la posici�n inicial de la aguja en el �ngulo m�nimo relativo a la rotaci�n actual
        transform.localRotation = minRotationQuaternion;
    }

    // M�todo para iniciar el movimiento de la aguja
    public void StartNeedleMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveNeedle());
        }
    }

    // M�todo para detener el movimiento y reiniciar la posici�n de la aguja
    public void ResetNeedle()
    {
        isMoving = false;
        StopAllCoroutines();
        transform.localRotation = minRotationQuaternion; // Regresa la aguja a la posici�n m�nima relativa a la rotaci�n inicial
    }

    private IEnumerator MoveNeedle()
    {
        isMoving = true;
        Quaternion targetRotation = maxRotationQuaternion;

        while (isMoving)
        {
            // Interpola la rotaci�n de la aguja hacia el �ngulo objetivo usando Quaternion.Lerp
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * speed);

            // Cambia la direcci�n de la aguja cuando alcanza el �ngulo objetivo
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                targetRotation = (targetRotation == maxRotationQuaternion) ? minRotationQuaternion : maxRotationQuaternion;
            }

            yield return null;
        }
    }
}
