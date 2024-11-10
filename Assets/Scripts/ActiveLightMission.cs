using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLightMission : MonoBehaviour
{
    public Light pickupLight;      // Luz para el punto de recogida
    public Light deliveryLight;    // Luz para el punto de entrega
    public Light finalObjectiveLight; // Luz para el objetivo final

    void Update()
    {
        UpdateMissionLights();
    }

    // Actualiza el estado de las luces en función del progreso de la misión
    void UpdateMissionLights()
    {
        if (!CargoMission.hasCargo && !CargoMission.headingToFinalObjective)
        {
            pickupLight.gameObject.SetActive(true);
        }
        else if (CargoMission.hasCargo && !CargoMission.headingToFinalObjective)
        {
            pickupLight.gameObject.SetActive(false);
            deliveryLight.gameObject.SetActive(true);
        }
        else if (CargoMission.headingToFinalObjective)
        {
            deliveryLight.gameObject.SetActive(false);
            finalObjectiveLight.gameObject.SetActive(true);
        }
    }
}
