using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] possibleObjectsPrefabs;

    [SerializeField] float spawnDelay = 2.5f;

    float timeSinceLastSpawn = 0.0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleObjectSpawning();
    }

    void HandleObjectSpawning()
    {
        if (timeSinceLastSpawn >= spawnDelay)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                
                int randomX = Random.Range(0, Screen.width);

                // Keeping the Y-level at 0.0f makes it so objects always spawn on top of the screen
                Vector2 randomScreenSpawnPosition = mainCamera.ScreenToWorldPoint(new Vector2(randomX, Screen.height));

                GameObject randomObjectPrefab = GetRandomObjectPrefab();
                if (randomObjectPrefab != null)
                {
                    Instantiate(randomObjectPrefab, randomScreenSpawnPosition, Quaternion.identity);
                }

            }
            timeSinceLastSpawn = 0.0f;
        }

        timeSinceLastSpawn += Time.deltaTime;

    }

    GameObject GetRandomObjectPrefab()
    {
        if (possibleObjectsPrefabs == null || possibleObjectsPrefabs.Length == 0) { return null; }

        return possibleObjectsPrefabs[Random.Range(0, possibleObjectsPrefabs.Length)];
    }
}
