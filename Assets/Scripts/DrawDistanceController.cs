using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDistanceController : MonoBehaviour
{
    public Camera mainCamera; // Cámara principal
    public float drawDistance = 50f; // Distancia máxima para renderizar el objeto
    private Renderer objectRenderer;

    void Start()
    {
        // Obtén el Renderer del objeto para activarlo/desactivarlo
        objectRenderer = GetComponent<Renderer>();

        // Si no se ha asignado una cámara, utilizamos la principal de la escena
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Calcula la distancia entre la cámara y el objeto
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Si la distancia es mayor al draw distance, desactiva el renderizador
        if (distance > drawDistance)
        {
            objectRenderer.enabled = false;
        }
        else
        {
            objectRenderer.enabled = true;
        }
    }
}
