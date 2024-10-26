using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Controller : MonoBehaviour
{
    public Transform airplane;
    public Text heightAirplane;
    public Text timer;
    private float timeElapsed = 0f;
    internal static bool gameEnd;

    public Transform target;

    public Text nombrePartida;
    public Text finalTimeText; // Texto para el tiempo total
    public Text averageHeightText; // Texto para la altura promedio
    public Text finalScoreText; // Texto para el puntaje final
    public Text distanceTarget; // Texto para la distancia al objetivo

    private float averageHeight;
    private float startTime;
    private float endTime;
    private float totalTime;
    private float totalHeight = 0f;
    private int heightSamples = 0;
    private static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
        // Registrar el tiempo al inicio del juego
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(airplane != null && Game_Controller.win == false)
        {
            float zAxis = airplane.position.y;
            heightAirplane.text = "Height: " + zAxis.ToString("F2");
            // Acumular la altura del avión en cada frame para calcular el promedio
            totalHeight += airplane.position.y;
            heightSamples++;

            timeElapsed += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);

            timer.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

            //Calcula la distancia entre el avion y el objetivo
            float distance = Vector3.Distance(target.position, airplane.position);
            distanceTarget.text = "Distancia hacia el objetivo: " + distance.ToString("F2") + " metros";
        }
        else if(airplane != null && Game_Controller.win == true)
        {
            timeElapsed = 0f;
        }
        EndGame();
    }

    // Método para sumar puntos al puntaje
    public static void AddScore(int points)
    {
        score += points;
    }

    // Método que se llama al finalizar el juego
    public void EndGame()
    {
        if(!gameEnd)
        {
            // Calcular el tiempo total
            endTime = Time.time;
            totalTime = endTime - startTime;

            // Calcular la altura promedio
            averageHeight = totalHeight / heightSamples;
        }
        else
        {
            finalTimeText.gameObject.SetActive(true);
            averageHeightText.gameObject.SetActive(true);
            finalScoreText.gameObject.SetActive(true);

            // Mostrar los valores en los textos del Canvas
            finalTimeText.text = "Tiempo total: " + totalTime.ToString("F2") + " segundos";
            averageHeightText.text = "Altura promedio: " + averageHeight.ToString("F2") + " metros";
            finalScoreText.text = "Puntaje final: " + score.ToString();
        }
    }
}
