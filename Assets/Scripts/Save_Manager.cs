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

    private const int maxPartidas = 5;  // M�ximo de partidas permitidas

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
            Debug.LogWarning("El nombre de la partida no puede estar vac�o.");
        }

    }

    // Elimina la partida m�s antigua si hay m�s de 5 archivos
    private void LimitarCantidadDePartidas()
    {
        string[] archivos = Directory.GetFiles(Application.persistentDataPath, "*.json");

        if (archivos.Length >= maxPartidas)
        {
            // Ordenar por fecha de creaci�n y eliminar el m�s antiguo
            string archivoMasAntiguo = archivos.OrderBy(f => File.GetCreationTime(f)).First();
            File.Delete(archivoMasAntiguo);
            Debug.Log($"Se elimin� la partida m�s antigua: {archivoMasAntiguo}");
        }
    }
}
