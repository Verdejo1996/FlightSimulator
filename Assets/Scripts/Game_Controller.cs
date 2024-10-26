using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public AirPlane_Controller airplane;
    private int landedCount;
    public Text winText;
    public Text loseText;
    public static bool win;

    // Start is called before the first frame update
    void Start()
    {
        landedCount = 0;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        WinCondition();
    }

    void WinCondition()
    {
        if (airplane != null)
        {
            if(landedCount == 1)
            {
                winText.gameObject.SetActive(true);
                win = true;
                Hud_Controller.gameEnd = true;
                OnGameOver();
            }
        }
        else
        {
            loseText.gameObject.SetActive(true);
            Hud_Controller.gameEnd = true;
            OnGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (airplane != null)
        {
            if (collision.collider.CompareTag("Airplane"))
            {
                landedCount++;
            }
        }
    }

    private void OnGameOver()
    {
        FindObjectOfType<Save_Manager>().MostarBtnGuardar();
    }
}
