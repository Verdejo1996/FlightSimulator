using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirPlane_Controller : MonoBehaviour
{
    public float speed = 0f;
    public float topSpeed = 20f;
    public float maxSpeedIncrement = 5f;
    public float defaultLift = 0.01f;
    public float flapSpeed = 2f;

    public float maxfuel = 100f;
    public float consumptionRate = 0.5f;
    private float currentFuel;

    public ParticleSystem explosion;

    public Slider speedSlider;
    public Slider fuelSlider;

    //public GameObject windZone;

    public GameObject leftWingFlap;
    public GameObject rightWingFlap;

    public GameObject tailFlap1;
    public GameObject tailFlap2;
    public GameObject tailFlap3;

    public GameObject[] propellers;

    float goingUp, rotateRight, slideRight, dynamicLift, fallRate, glideRate;
    bool isGrounded, isMovingForward; //inWindZone = false;

    float speedIncrement = 0f;
    float previousSpeedIncrement;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goingUp = 0f;
        rotateRight = 0f;

        explosion.Pause();

        currentFuel = maxfuel;
        fuelSlider.maxValue = maxfuel;
        fuelSlider.value = maxfuel;

        speedSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void Update()
    {
        ConsumeFuel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
/*        if (isMovingForward)
        {
            speed += speedIncrement * Time.deltaTime;
        }
        else
        {
            speed -= speed * Time.deltaTime;
        }*/

        speed = Mathf.Clamp(speed, 0f, topSpeed);

        //transform.Translate(0, 0, (speed + (glideRate*10)) * Time.deltaTime);

        /*        if(Input.GetKey(KeyCode.Space))
                {
                    transform.Translate(0, 0, (speed + (glideRate * 25)) * Time.deltaTime);
                }
                else if(speed == 0)
                {
                    transform.Translate(0, 0, 0);
                }*/
        if(speed > 5)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (speed > 10)
        {
            transform.Translate(0, 0, (speed + (glideRate * 25)) * Time.deltaTime);
        }
/*        else if (speed == 0)
        {
            transform.Translate(0, 0, 0);
        }*/

        if (isGrounded)
        {
            //Rotamos el transform para que levante la nariz del avion y pueda volar.
            transform.Rotate(dynamicLift, 0, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }

        //Agregamos velocidad al rigidbody para que avance hacia delante.
        //Cuando el valor de fallRate es 1 el avion cae
        rb.velocity = new Vector3(0, rb.velocity.y * fallRate, 0);

        //Movimiento del avion.
        #region
        //Aceleracion y desaceleracion
        if (Input.GetKey(KeyCode.Space))
        {
            speedSlider.value += 0.005f;
            speed += speedIncrement * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            speedSlider.value -= 0.005f;
            speed -= maxSpeedIncrement * Time.deltaTime;
            if (speed == 0)
            {
                transform.Translate(0, 0, 0);
            }
        }
        //Down
        if (Input.GetKey(KeyCode.W) || goingUp > 0f)
        {
            //Rotacion de los flaps izquierdos.
            if (leftWingFlap.transform.localEulerAngles.x > 315 || leftWingFlap.transform.localEulerAngles.x < 180)
            {
                leftWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }
            //Rotacion de los flaps derechos
            if (rightWingFlap.transform.localEulerAngles.x > 315 || rightWingFlap.transform.localEulerAngles.x < 180)
            {
                rightWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }
            //Flaps traseros
            if (tailFlap2.transform.localEulerAngles.x > 315 || tailFlap2.transform.localEulerAngles.x < 180)
            {
                tailFlap2.transform.Rotate(-flapSpeed, 0, 0);
            }
            if (tailFlap3.transform.localEulerAngles.x > 315 || tailFlap3.transform.localEulerAngles.x < 180)
            {
                tailFlap3.transform.Rotate(-flapSpeed, 0, 0);
            }

            if (speed > topSpeed / 2)
            {
                transform.Rotate(0.5f, 0, 0);
            }
        }
        //Up
        else if (Input.GetKey(KeyCode.S) || goingUp < 0f)
        {
            //Rotacion de los flaps izquierdos.
            if (leftWingFlap.transform.localEulerAngles.x < 45 || leftWingFlap.transform.localEulerAngles.x > 180)
            {
                leftWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }
            //Rotacion de los flaps derechos
            if (rightWingFlap.transform.localEulerAngles.x < 45 || rightWingFlap.transform.localEulerAngles.x > 180)
            {
                rightWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }
            //Flaps traseros
            if (tailFlap2.transform.localEulerAngles.x < 45 || tailFlap2.transform.localEulerAngles.x > 180)
            {
                tailFlap2.transform.Rotate(flapSpeed, 0, 0);
            }
            if (tailFlap3.transform.localEulerAngles.x < 45 || tailFlap3.transform.localEulerAngles.x > 180)
            {
                tailFlap3.transform.Rotate(flapSpeed, 0, 0);
            }

            if (speed > topSpeed / 2)
            {
                transform.Rotate(-0.5f, 0, 0);
            }
        }
        //RotateLeft
        else if (Input.GetKey(KeyCode.A) || rotateRight < 0f)
        {
            transform.Rotate(-0.1f, -0.1f, 0);
            //Rotacion de los flaps izquierdos.
            if (leftWingFlap.transform.localEulerAngles.x < 45 || leftWingFlap.transform.localEulerAngles.x > 180)
            {                
                leftWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }
            //Rotacion de los flaps derechos
            if(rightWingFlap.transform.localEulerAngles.x > 315 || rightWingFlap.transform.localEulerAngles.x < 180)
            {
                rightWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }

            if (speed > topSpeed / 2)
            {
                transform.Rotate(0, 0, 0.5f);
            }
        }
        //RotateRight
        else if (Input.GetKey(KeyCode.D) || rotateRight > 0f)
        {
            transform.Rotate(-0.1f, 0.1f, 0);
            //Rotacion de los flaps derechos.
            if (leftWingFlap.transform.localEulerAngles.x > 315 || leftWingFlap.transform.localEulerAngles.x < 180)
            {                
                leftWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }
            if (rightWingFlap.transform.localEulerAngles.x < 45 || rightWingFlap.transform.localEulerAngles.x > 180)
            {
                rightWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }

            if (speed > topSpeed / 2)
            {
                transform.Rotate(0, 0, -0.5f);
            }
        }
        //SlideLeft
        else if (Input.GetKey(KeyCode.Q) || slideRight < 0f)
        {
            if (speed > 5)
            {
                if (tailFlap1.transform.localEulerAngles.y < 45 || tailFlap1.transform.localEulerAngles.y > 180)
                {
                    tailFlap1.transform.Rotate(0, flapSpeed, 0);
                }
                transform.Rotate(0, -0.5f, 0);
            }
        }
        //SlideRight
        else if (Input.GetKey(KeyCode.E) || slideRight > 0f)
        {
            if (speed > 5)
            {
                if (tailFlap1.transform.localEulerAngles.y > 315 || tailFlap1.transform.localEulerAngles.y < 180)
                {
                    tailFlap1.transform.Rotate(0, -flapSpeed, 0);
                }
                transform.Rotate(0, 0.5f, 0);
            }
        }
        else
        {
            if(leftWingFlap.transform.localRotation.x < 0)
            {
                leftWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }
            if (rightWingFlap.transform.localRotation.x < 0)
            {
                rightWingFlap.transform.Rotate(flapSpeed, 0, 0);
            }
            if (leftWingFlap.transform.localRotation.x > 0)
            {
                leftWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }
            if (rightWingFlap.transform.localRotation.x > 0)
            {
                rightWingFlap.transform.Rotate(-flapSpeed, 0, 0);
            }
            if (tailFlap1.transform.localRotation.y > 0)
            {
                tailFlap1.transform.Rotate(0, -flapSpeed, 0);
            }
            if (tailFlap1.transform.localRotation.y < 0)
            {
                tailFlap1.transform.Rotate(0, flapSpeed, 0);
            }
            if (tailFlap2.transform.localRotation.x < 0)
            {
                tailFlap2.transform.Rotate(flapSpeed, 0, 0);
            }
            if (tailFlap2.transform.localRotation.x > 0)
            {
                tailFlap2.transform.Rotate(-flapSpeed, 0, 0);
            }
            if (tailFlap3.transform.localRotation.x < 0)
            {
                tailFlap3.transform.Rotate(flapSpeed, 0, 0);
            }
            if (tailFlap3.transform.localRotation.x > 0)
            {
                tailFlap3.transform.Rotate(-flapSpeed, 0, 0);
            }
        }
        #endregion

        //Propeller rotation
        for(int i = 0; i < propellers.Length; i++)
        {
            propellers[i].transform.Rotate(0, 0, (speed * 5f) + 3f);
        }

/*        if (inWindZone)
        {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }*/
    }

    public void GoUpDown(float v)
    {
        goingUp = v;
    }

    public void GoLeftRight(float v)
    {
        rotateRight = v;
    }

    public void SlideLeftRight(float v)
    {
        slideRight = v;
    }

    public void ValueChangeCheck()
    {
        speedIncrement = speedSlider.value * maxSpeedIncrement;

        if(speedIncrement > previousSpeedIncrement)
        {
            isMovingForward = true;
        }
        else if(speedIncrement < previousSpeedIncrement)
        {
            isMovingForward = false;
        }

        previousSpeedIncrement = speedIncrement;

        dynamicLift = speedSlider.value * defaultLift;

        fallRate = 1f - speedSlider.value;

        if(speed < 5)
        {
            fallRate = 1f;
        }

        if(!isGrounded)
        {
            glideRate = 1f - speedSlider.value;
        }
    }

    void ConsumeFuel()
    {
        if(currentFuel > 0)
        {
            if(isMovingForward)
            {
                //Reduce el combustible con base en la tasa de consumo.
                currentFuel -= consumptionRate * Time.deltaTime;
                fuelSlider.value = currentFuel;
            }
        }
        else
        {
            currentFuel = 0;
            fallRate = 1;
            speed = 0;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Runway")
        {
            isGrounded = true;
        }
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "EndMap" || collision.gameObject.tag == "Drone")
        {
            isGrounded = true;
            glideRate = 0;
            OnDestroy();
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnDestroy()
    {
        /*Desacopla el Particle System del objeto, luego lo reproduce y por ultimo
        lo destruye despues de finalizar.*/
        explosion.transform.parent = null;
        explosion.Play();
        Destroy(explosion.gameObject, explosion.main.duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel"))
        {
            currentFuel += 10f;
            fuelSlider.value = currentFuel;
            other.gameObject.SetActive(false);
        }
        else
        {
            ConsumeFuel();
        }

/*        if(other.gameObject.tag == "WindArea")
        {
            windZone = other.gameObject;
            inWindZone = true;
        }*/
    }

/*    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WindArea")
        {
            inWindZone = false;
        }
    }*/

}
