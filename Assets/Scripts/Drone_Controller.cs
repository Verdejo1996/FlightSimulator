using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Transform player;              // Referencia al avi�n del jugador
    public float speed = 10f;              // Velocidad del drone
    public float detectionRadius = 250f;   // Radio de detecci�n del jugador
    public float randomMoveInterval = 10f; // Tiempo entre movimientos aleatorios

    private Vector3 targetPosition;       // Posici�n hacia la que se mueve el drone
    private float lastMoveTime;           // Tiempo del �ltimo movimiento

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Airplane").transform;
        }

        GenerateRandomTarget();
        lastMoveTime = Time.time;
    }

    void Update()
    {
        if (Hud_Controller.gameEnd)
        {
            GenerateRandomTarget();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Si el jugador est� cerca, sigue al jugador
            targetPosition = player.position;
        }
        else
        {
            // Movimiento aleatorio si el jugador no est� cerca
            if (Time.time - lastMoveTime > randomMoveInterval)
            {
                GenerateRandomTarget();
                lastMoveTime = Time.time;
            }
        }

        MoveDrone();
    }

    void MoveDrone()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void GenerateRandomTarget()
    {
        // Genera un movimiento aleatorio alrededor de la posici�n actual
        float randomX = Random.Range(-100f, 100f);
        float randomZ = Random.Range(-100f, 100f);
        float randomY = Random.Range(-10f, 10f); // Permitir variaci�n en altura
        targetPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
