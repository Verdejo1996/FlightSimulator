using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCircle_Controller : MonoBehaviour
{
    private int points;
    //public Text pText;
    public GameObject pCircle;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //pText.text = points.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Airplane"))
        {
            points++;
            Debug.Log(points);
            pCircle.SetActive(false);
        }

        Hud_Controller.AddScore(points);
    }


/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Airplane"))
        {
            points++;
            Hud_Controller.AddScore(points);
            Debug.Log(points);
            pCircle.SetActive(false);
        }
    }*/
}
