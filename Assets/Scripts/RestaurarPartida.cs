using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurarPartida : MonoBehaviour
{
    public Hud_Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PartidaCargada"))
        {
            string json = PlayerPrefs.GetString("PartidaCargada");
            Save_Controller partida = JsonUtility.FromJson<Save_Controller>(json);

            partida.nombrePartida = controller.nombrePartida.text;
            partida.finalTime = controller.finalTimeText.text;
            partida.averageHeight = controller.averageHeightText.text;
            partida.points = controller.finalScoreText.text;

            Debug.Log($"Partida '{partida.nombrePartida}' cargada.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
