using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Controller : MonoBehaviour
{
    public float dayDuration = 120f; // Duraci�n total de un ciclo d�a/noche en segundos
    public Light sun; // Directional Light que representa el Sol
    public Gradient skyColor; // Color del cielo durante el d�a y la noche

    public float maxIntensity = 2f; // Intensidad m�xima de la luz
    public float minIntensity = 0.2f; // Intensidad m�nima de la luz (opcional para evitar oscuridad total)

    private float timeElapsed = 0f;

    void Update()
    {
        // Avanzar el tiempo del ciclo
        timeElapsed += Time.deltaTime;

        // Calcular el progreso del d�a (valor entre 0 y 1)
        float dayProgress = (timeElapsed / dayDuration) % 1;

        // Ajustar la rotaci�n del sol (modificamos para un ciclo m�s realista)
        sun.transform.localRotation = Quaternion.Euler((dayProgress * 360f) - 90f, 0f, 0f);

        // Calcular la intensidad de la luz usando el seno, con valores entre min y max
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Clamp01(Mathf.Sin(dayProgress * Mathf.PI)));

        // Asignar la intensidad calculada al sol
        sun.intensity = intensity;

        // Ajustar el color ambiental del cielo seg�n el gradiente
        RenderSettings.ambientLight = skyColor.Evaluate(dayProgress);

        RenderSettings.fogDensity = Mathf.Lerp(0f, 0.02f, 1f - sun.intensity);
    }
}
