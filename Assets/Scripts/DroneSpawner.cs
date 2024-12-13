using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public GameObject dronePrefab;        // Prefab del drone
    public BoxCollider spawnArea;        // Box Collider del �rea de spawn
    public int numberOfDrones = 5;       // Cantidad inicial de drones
    public float spawnHeight = 10f;      // Altura a la que aparecer�n los drones
    public float spawnInterval = 10f; // Tiempo entre spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnDrone), 0f, spawnInterval);
    }

    void SpawnDrone()
    {
        // Genera una posici�n aleatoria dentro del �rea del Box Collider
        Vector3 randomPosition = GetRandomPositionInArea();

        // Instancia el drone en la posici�n calculada
        Instantiate(dronePrefab, randomPosition, Quaternion.identity);

        // Asigna din�micamente el objetivo al script del drone
        DroneController controller = dronePrefab.GetComponent<DroneController>();
        if (controller != null)
        {
            controller.player = GameObject.FindWithTag("Airplane").transform;
        }
    
    }

    Vector3 GetRandomPositionInArea()
    {
        // Obtiene los l�mites del Box Collider
        Bounds bounds = spawnArea.bounds;

        // Calcula una posici�n aleatoria dentro de los l�mites
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Mant�n la altura en un valor fijo o ajustable
        float y = bounds.center.y + spawnHeight;

        return new Vector3(randomX, y, randomZ);
    }
}


