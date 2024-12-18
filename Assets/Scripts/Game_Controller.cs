using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        if (SceneManager.GetActiveScene().name == "NivelTutorial")
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(NextLevelAfterDelay(1.0f));
            }
        }

        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(NextLevelAfterDelay(1.0f));
            }
        }
    }

    void WinCondition()
    {
        if(SceneManager.GetActiveScene().name == "Nivel1")
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

        if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            if (airplane != null)
            {
                if (landedCount == 1)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (airplane != null)
        {
            if (collision.collider.CompareTag("Airplane"))
            {
                landedCount++;
                if(SceneManager.GetActiveScene().name == "NivelTutorial")
                {
                    Hud_Controller.gameEnd = true;
                }
            }
        }
    }

    IEnumerator NextLevelAfterDelay(float delay)
    {
        if (SceneManager.GetActiveScene().name == "NivelTutorial")
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene("Nivel1");
        }
        else
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene("Nivel2");
        }
    }


    private void OnGameOver()
    {
        FindObjectOfType<Save_Manager>().MostarBtnGuardar();
    }
}
