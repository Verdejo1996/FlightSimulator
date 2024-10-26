using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DropDownFillGames : MonoBehaviour
{
    public Dropdown dropdown;
    public Button btnConfirmar;
    public Button btnEliminar;

    // Start is called before the first frame update
    void Start()
    {
        FillDropDown();
        btnConfirmar.onClick.AddListener(CargarPartidaSeleccionada);
        btnEliminar.onClick.AddListener(EliminarPartidaSeleccionada);
    }

    void FillDropDown()
    {
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.json");

        var options = files.Select(Path.GetFileNameWithoutExtension).ToList();

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void CargarPartidaSeleccionada()
    {
        string nombreArchivo = dropdown.options[dropdown.value].text;
        string path = Path.Combine(Application.persistentDataPath , nombreArchivo + ".json");

        if(System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            Save_Controller partida = JsonUtility.FromJson<Save_Controller>(json);

            PlayerPrefs.SetString("PartidaCargada", json);

            SceneManager.LoadScene(partida.nombreEscena);
        }
        else
        {
            Debug.LogError($"No se encontro el archivo en la ruta: {path}.");
        }
    }

    void EliminarPartidaSeleccionada()
    {
        string nombreArchivo = dropdown.options[dropdown.value].text;
        string path = Path.Combine(Application.persistentDataPath, nombreArchivo + ".json");

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
            Debug.Log($"Partida '{nombreArchivo}' eliminada.");

            // Actualizar el dropdown después de eliminar
            FillDropDown();
        }
        else
        {
            Debug.LogError("No se encontró el archivo para eliminar.");
        }
    }
}
