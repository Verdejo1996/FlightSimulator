using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnKeySync : MonoBehaviour
{
    public Button buttonA; // Botón en el canvas que representa la tecla A
    public Button buttonW; // Botón para la tecla W
    public Button buttonS; // Botón para la tecla S
    public Button buttonD; // Botón para la tecla D
    public Button buttonQ; // Botón para la tecla D
    public Button buttonE; // Botón para la tecla D

    private Color defaultColor = Color.white; // Color original del botón
    private Color pressedColor = Color.green; // Color al presionar el botón

    void Update()
    {
        // Detectar si se presiona la tecla A
        if (Input.GetKey(KeyCode.A))
        {
            SimulateButtonPress(buttonA);
        }
        else
        {
            ResetButtonColor(buttonA);
        }

        // Detectar si se presiona la tecla W
        if (Input.GetKey(KeyCode.W))
        {
            SimulateButtonPress(buttonW);
        }
        else
        {
            ResetButtonColor(buttonW);
        }

        // Detectar si se presiona la tecla S
        if (Input.GetKey(KeyCode.S))
        {
            SimulateButtonPress(buttonS);
        }
        else
        {
            ResetButtonColor(buttonS);
        }

        // Detectar si se presiona la tecla D
        if (Input.GetKey(KeyCode.D))
        {
            SimulateButtonPress(buttonD);
        }
        else
        {
            ResetButtonColor(buttonD);
        }

        // Detectar si se presiona la tecla Q
        if (Input.GetKey(KeyCode.Q))
        {
            SimulateButtonPress(buttonQ);
        }
        else
        {
            ResetButtonColor(buttonQ);
        }

        // Detectar si se presiona la tecla E
        if (Input.GetKey(KeyCode.E))
        {
            SimulateButtonPress(buttonE);
        }
        else
        {
            ResetButtonColor(buttonE);
        }
    }

    // Simula el cambio visual de presionar un botón
    void SimulateButtonPress(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = pressedColor; // Cambiar el color del botón al presionar
        button.colors = cb;
    }

    // Restablece el color original del botón cuando no está presionado
    void ResetButtonColor(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = defaultColor; // Volver al color original
        button.colors = cb;
    }
}
