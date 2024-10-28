using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Controller : MonoBehaviour
{
    public float dayDuration = 120f; // Duración total de un ciclo día/noche en segundos
    public Light sun; // Directional Light que representa el Sol
    public Gradient skyColor; // Color del cielo durante el día y la noche

    public float maxIntensity = 2f; // Intensidad máxima de la luz
    public float minIntensity = 0.2f; // Intensidad mínima de la luz (opcional para evitar oscuridad total)

    private float timeElapsed = 0f;

    // Desplazamiento inicial para que comience de día (0 = medianoche, 0.25 = amanecer, 0.5 = mediodía)
    public float initialProgress = 0.25f;

    void Update()
    {
        // Avanzar el tiempo del ciclo
        timeElapsed += Time.deltaTime;

        // Calcular el progreso del ciclo de día (valor entre 0 y 1)
        float dayProgress = (timeElapsed / dayDuration + initialProgress) % 1;

        // Rotar el Sol para que siga el ciclo del día y la noche
        sun.transform.localRotation = Quaternion.Euler(dayProgress * 360f - 90f, 0f, 0f);

        // Ajustar la intensidad del Sol usando una función seno (suave transición)
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Clamp01(Mathf.Sin(dayProgress * Mathf.PI)));

        // Aplicar la intensidad calculada
        sun.intensity = intensity;

        // Ajustar el color del cielo con el gradiente según el progreso del día
        RenderSettings.ambientLight = skyColor.Evaluate(dayProgress);

        RenderSettings.fogDensity = Mathf.Lerp(0f, 0.02f, 1f - sun.intensity);
    }
}
