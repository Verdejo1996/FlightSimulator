using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDialog_Controller : MonoBehaviour
{
    public Text controlTowerText;  // Referencia al texto UI en el Canvas
    public float messageDuration = 5f;        // Duración de cada mensaje en pantalla
    public Transform player;                  // Referencia al jugador (avión)

    // Lista de mensajes y sus condiciones de activación
    [System.Serializable]
    public class TowerMessage
    {
        public string message;                // Mensaje de la torre
        public Transform triggerZone;         // Zona de activación (si aplica)
        public float triggerDistance = 0f;    // Distancia mínima para activar (si aplica)
        public bool hasBeenDisplayed = false; // Control interno para mostrar mensaje una vez
    }
    public List<TowerMessage> messages;       // Lista de mensajes con condiciones

    void Update()
    {
        CheckForMessageTriggers();
    }

    // Revisa las condiciones de activación de los mensajes
    void CheckForMessageTriggers()
    {
        foreach (TowerMessage towerMessage in messages)
        {
            if (player != null && Game_Controller.win == false)
            {
                if (!towerMessage.hasBeenDisplayed)
                {
                    // Si tiene una zona de activación
                    if (towerMessage.triggerZone != null)
                    {
                        float distance = Vector3.Distance(player.position, towerMessage.triggerZone.position);
                        Debug.Log($"Distancia al trigger '{towerMessage.message}': {distance}");

                        if (distance <= towerMessage.triggerDistance)
                        {
                            Debug.Log($"Activando mensaje: {towerMessage.message}");
                            DisplayMessage(towerMessage.message);
                            towerMessage.hasBeenDisplayed = true;  // Marca el mensaje como mostrado
                        }
                    }
                }
            }
        }
    }

    // Muestra el mensaje en pantalla y lo limpia después de un tiempo
    void DisplayMessage(string message)
    {
        controlTowerText.text = message;
        StartCoroutine(ClearMessageAfterDelay());
    }

    IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        controlTowerText.text = "";
    }
}
