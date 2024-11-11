using UnityEngine;
using UnityEngine.UI;

public class CargoMission : MonoBehaviour
{
    public Transform player;                     // Referencia al avión o jugador
    public Transform pickupPoint;                // Punto de recogida de la carga
    public Transform deliveryPoint;              // Punto de entrega de la carga
    public Transform finalObjective;             // Objetivo final del aterrizaje
    public Text missionStatusText;    // Texto para mostrar el estado de la misión en la UI
    public Text distanceText;         // Texto para mostrar la distancia al objetivo en la UI
    public float interactionDistance = 10f;      // Distancia para recoger/entregar la carga

    public GameObject package;

    internal static bool hasCargo = false;               // Si el jugador tiene la carga
    internal static bool missionCompleted = false;       // Si la misión fue completada
    internal static bool headingToFinalObjective = false;// Si el jugador va hacia el objetivo final

    void Update()
    {
        if (!missionCompleted)
        {
            UpdateMissionText();
            CheckForPickup();
            CheckForDelivery();
        }
    }

    // Actualiza el texto de la misión y de la distancia según el estado actual
    void UpdateMissionText()
    {
        Transform currentTarget = null;

        if (player != null)
        {
            if (!hasCargo && !headingToFinalObjective)
            {
                currentTarget = pickupPoint;
                missionStatusText.text = "Dirígete al punto de recogida.";
            }
            else if (hasCargo && !headingToFinalObjective)
            {
                currentTarget = deliveryPoint;
                missionStatusText.text = "Dirígete al punto de entrega.";
            }
            else if (!hasCargo && headingToFinalObjective)
            {
                currentTarget = finalObjective;
                missionStatusText.text = "Dirígete al objetivo final para aterrizar.";
            }

            if (currentTarget != null)
            {
                float distance = Vector3.Distance(player.position, currentTarget.position);
                distanceText.text = $"Distancia al objetivo: {distance:F2} m";
            }
        }
    }

    // Comprobar si el jugador está cerca del punto de recogida para recoger la carga
    void CheckForPickup()
    {
        if (player != null)
        {
            if (!hasCargo)
            {
                float distanceToPickup = Vector3.Distance(player.position, pickupPoint.position);

                if (distanceToPickup <= interactionDistance)
                {
                    missionStatusText.text = "Presiona 'E' para recoger la carga";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        package.SetActive(false);
                        hasCargo = true;
                        missionStatusText.text = "Carga recogida. Dirígete al punto de entrega.";
                    }
                }
            }
        }
    }

    // Comprobar si el jugador está cerca del punto de entrega para entregar la carga
    void CheckForDelivery()
    {
        if (player != null)
        {
            if (hasCargo && !headingToFinalObjective)
            {
                float distanceToDelivery = Vector3.Distance(player.position, deliveryPoint.position);

                if (distanceToDelivery <= interactionDistance)
                {
                    missionStatusText.text = "Presiona 'E' para entregar la carga";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        missionStatusText.text = "Carga entregada. Dirígete al objetivo final para aterrizar.";
                        package.transform.position = deliveryPoint.position;
                        package.SetActive(true);
                        hasCargo = false;
                        headingToFinalObjective = true;
                    }
                }
            }
        }
    }
}
