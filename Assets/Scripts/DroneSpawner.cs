using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public GameObject dronePrefab;        // Prefab del drone
    public BoxCollider spawnArea;        // Box Collider del área de spawn
    public int numberOfDrones = 5;       // Cantidad inicial de drones
    public float spawnHeight = 10f;      // Altura a la que aparecerán los drones
    public float spawnInterval = 10f; // Tiempo entre spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnDrone), 0f, spawnInterval);
    }

    void SpawnDrone()
    {
        // Genera una posición aleatoria dentro del área del Box Collider
        Vector3 randomPosition = GetRandomPositionInArea();

        // Instancia el drone en la posición calculada
        Instantiate(dronePrefab, randomPosition, Quaternion.identity);

        // Asigna dinámicamente el objetivo al script del drone
        DroneController controller = dronePrefab.GetComponent<DroneController>();
        if (controller != null)
        {
            controller.player = GameObject.FindWithTag("Airplane").transform;
        }
    
    }

    Vector3 GetRandomPositionInArea()
    {
        // Obtiene los límites del Box Collider
        Bounds bounds = spawnArea.bounds;

        // Calcula una posición aleatoria dentro de los límites
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Mantén la altura en un valor fijo o ajustable
        float y = bounds.center.y + spawnHeight;

        return new Vector3(randomX, y, randomZ);
    }
}


