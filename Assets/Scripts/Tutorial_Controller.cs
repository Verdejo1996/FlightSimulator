using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Controller : MonoBehaviour
{
    public Text tutorialText; // Referencia al texto del tutorial en el Canvas
    public float delayBetweenSteps = 2f; // Tiempo entre cada paso del tutorial
    private int stepIndex = 0; // El índice del paso actual del tutorial
    private bool isTutorialActive = true; // Controla si el tutorial está activo
    private bool allowControl = false; // Permitir o no el control del avión

    void Start()
    {
        // Comenzar el tutorial
        StartCoroutine(ShowNextTutorialStep());
    }

    void Update()
    {
        // Si el tutorial está activo y el jugador puede realizar acciones
        if (isTutorialActive && allowControl)
        {
            switch (stepIndex)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.Space)) // Acelerar
                    {
                        allowControl = false; // Deshabilitar los controles mientras mostramos la próxima instrucción
                        StartCoroutine(ShowNextTutorialStep());
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.S)) // Mover hacia abajo
                    {
                        allowControl = false;
                        StartCoroutine(ShowNextTutorialStep());
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.W)) // Mover hacia arriba
                    {
                        allowControl = false;
                        StartCoroutine(ShowNextTutorialStep());
                    }
                    break;

                case 4:
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E)) // Mover hacia la derecha
                    {
                        allowControl = false;
                        StartCoroutine(ShowNextTutorialStep());
                    }
                    break;

                case 5:
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q)) // Mover hacia la izquierda
                    {
                        allowControl = false;
                        StartCoroutine(ShowNextTutorialStep());
                    }
                    break;

                case 6:
                    if (Input.GetKeyDown(KeyCode.P)) // Frenar
                    {
                        allowControl = false;
                        EndTutorial();
                    }
                    break;
            }
        }
        CirclesTutorial();
    }

    IEnumerator ShowNextTutorialStep()
    {
        // Mostrar el texto adecuado dependiendo del paso actual
        switch (stepIndex)
        {
            case 0:
                tutorialText.text = "Presiona Espacio para Acelerar.";
                break;
            case 1:
                tutorialText.text = "Presiona S para mover hacia arriba.";
                break;
            case 2:
                tutorialText.text = "Presiona W para mover hacia abajo.";
                break;
            case 3:
                tutorialText.text = "Presiona D o E para mover hacia la derecha.";
                break;
            case 4:
                tutorialText.text = "Presiona A o Q para mover hacia la izquierda.";
                break;
            case 5:
                tutorialText.text = "Presiona P para frenar.";
                break;
            case 6:
                tutorialText.text = "Puedes pasar por el circulo rojo para sumar puntos.";
                break;
            case 7:
                tutorialText.text = "Puedes pasar por el circulo verde para cargar combustible.";
                break;
        }

        // Esperar unos segundos en tiempo real antes de permitir la próxima acción
        yield return new WaitForSecondsRealtime(delayBetweenSteps);

        stepIndex++;  // Avanzar al siguiente paso
        allowControl = true; // Permitir el control del jugador nuevamente
    }

    void CirclesTutorial()
    {
        if(!isTutorialActive)
        {
            allowControl = false;
            StartCoroutine(ShowNextTutorialStep());
        }
    }

    void EndTutorial()
    {
        // Terminar el tutorial y continuar con el juego
        isTutorialActive = false;
        tutorialText.gameObject.SetActive(false);
        Debug.Log("Tutorial terminado");
    }
}
