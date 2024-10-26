using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Controller : MonoBehaviour
{
    public GameObject panelEstadisticas;  // Panel que muestra las estad�sticas
    public Text textoEstadisticas;        // Texto donde mostraremos las estad�sticas
    public Button botonPausar;            // Bot�n para pausar el juego
    public Button botonReanudar;          // Bot�n para reanudar el juego

    private void Start()
    {
        // Asegurarnos de que el panel de estad�sticas est� desactivado al inicio
        panelEstadisticas.SetActive(false);

        // Asignar los eventos de los botones
        botonPausar.onClick.AddListener(PausarYMostrarEstadisticas);
        botonReanudar.onClick.AddListener(ReanudarJuego);
    }

    // Pausar el juego y mostrar las estad�sticas
    public void PausarYMostrarEstadisticas()
    {
        Time.timeScale = 0;  // Frenar el tiempo

        // Cargar las estad�sticas desde PlayerPrefs
        if (PlayerPrefs.HasKey("PartidaCargada"))
        {
            string json = PlayerPrefs.GetString("PartidaCargada");
            Save_Controller partida = JsonUtility.FromJson<Save_Controller>(json);

            // Mostrar las estad�sticas en el panel
            textoEstadisticas.text = $"Nombre: {partida.nombrePartida}\n" +
                                     $"{partida.finalTime}\n" +
                                     $"{partida.averageHeight}\n" +
                                     $"{partida.points}\n";
        }
        else
        {
            textoEstadisticas.text = "No se encontraron datos de la partida.";
        }

        // Mostrar el panel de estad�sticas
        panelEstadisticas.SetActive(true);
    }

    // Reanudar el juego al cerrar el panel
    public void ReanudarJuego()
    {
        panelEstadisticas.SetActive(false);  // Ocultar el panel
        Time.timeScale = 1;  // Reanudar el tiempo
    }
}
