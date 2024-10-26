using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Save_Manager : MonoBehaviour
{
    public Hud_Controller controller;

    public GameObject btnGuardar;
    public GameObject panelInput;
    public InputField inputNombrePartida;
    public Button btnConfirmar;

    private const int maxPartidas = 5;  // Máximo de partidas permitidas

    private void Start()
    {
        btnGuardar.SetActive(false);
        panelInput.SetActive(false);

        btnConfirmar.onClick.AddListener(SaveGame);
    }

    public void MostarBtnGuardar()
    {
        btnGuardar.SetActive(true);
    }

    public void MostarInputField()
    {
        btnGuardar.SetActive(false);
        panelInput.SetActive(true);
    }
    public void SaveGame()
    {
        string nombreArchivo = inputNombrePartida.text;
        if(!string.IsNullOrEmpty(nombreArchivo))
        {
            string path = Path.Combine(Application.persistentDataPath, nombreArchivo + ".json");
            LimitarCantidadDePartidas();

            Save_Controller save = new Save_Controller()
            {
                nombrePartida = nombreArchivo,
                finalTime = controller.finalTimeText.text,
                averageHeight = controller.averageHeightText.text,
                points = controller.finalScoreText.text
            };

            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(path, json);

            Debug.Log($"Partida guardada como: {path}");
            panelInput.SetActive(false);
        }
        else
        {
            Debug.LogWarning("El nombre de la partida no puede estar vacío.");
        }

    }

    // Elimina la partida más antigua si hay más de 5 archivos
    private void LimitarCantidadDePartidas()
    {
        string[] archivos = Directory.GetFiles(Application.persistentDataPath, "*.json");

        if (archivos.Length >= maxPartidas)
        {
            // Ordenar por fecha de creación y eliminar el más antiguo
            string archivoMasAntiguo = archivos.OrderBy(f => File.GetCreationTime(f)).First();
            File.Delete(archivoMasAntiguo);
            Debug.Log($"Se eliminó la partida más antigua: {archivoMasAntiguo}");
        }
    }
}
