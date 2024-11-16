using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
    public XRGrabInteractable grabInteractHandleA;
    public XRGrabInteractable grabInteractHandleB;
    public Transform doorTransform; // Transform de la puerta
    public float openAngle = 90f;   // Ángulo máximo de apertura
    public float openSpeed = 2f;    // Velocidad de apertura

    private bool isOpening = false;
    private bool isClosing = false;
    private float currentAngle = 0f;

    void Start()
    {
        grabInteractHandleA.selectEntered.AddListener(x => StartOpening());
        grabInteractHandleB.selectEntered.AddListener(x => StartOpening());

        grabInteractHandleA.selectExited.AddListener(x => StopOpening());
        grabInteractHandleB.selectExited.AddListener(x => StopOpening());
    }

    void Update()
    {
        if (isOpening && currentAngle < openAngle)
        {
            float angleToOpen = openSpeed * Time.deltaTime;
            doorTransform.Rotate(Vector3.up, angleToOpen);
            currentAngle += angleToOpen;
        }
        else if (isClosing && currentAngle > 0)
        {
            float angleToClose = openSpeed * Time.deltaTime;
            doorTransform.Rotate(Vector3.up, -angleToClose);
            currentAngle -= angleToClose;
        }
    }

    private void StartOpening()
    {
        isOpening = true;
        isClosing = false;
    }

    private void StopOpening()
    {
        isOpening = false;
        isClosing = true;
    }
}
