using System.Collections.Generic;
using UnityEngine;

public class RoadSceneControl : MonoBehaviour
{
    [Header("Object Spawning")]
    public List<GameObject> elemsPrefabs = new List<GameObject>();
    public List<GameObject> currentElems = new List<GameObject>();

    [Header("Movement")]
    [Tooltip("Movement speed of objects towards the player.")]
    public float speed = 5f;
    [Tooltip("The Z-coordinate where objects will be spawned.")]
    public float spawnZ = 20f;

    [Header("Spawn Settings")]
    [Tooltip("Target spawn frequency (objects per second).")]
    public float spawnRate = 15f;

    [Header("Removal Settings")]
    [Tooltip("The Z-coordinate at which the object will be destroyed.")]
    public float destroyZ = -10f;

    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        float spawnInterval = 1f / spawnRate;

        if (spawnTimer >= spawnInterval && elemsPrefabs.Count > 0)
        {
            SpawnRandomObject();
            spawnTimer = 0f;
        }

        MoveAndRemoveObjects();
    }

    private void SpawnRandomObject()
    {
        GameObject prefab = elemsPrefabs[Random.Range(0, elemsPrefabs.Count)];

        float randomX = Random.Range(-4.5f, 4.5f);

        Vector3 spawnPosition = new Vector3(randomX, 0f, spawnZ);

        if (prefab.TryGetComponent(out Renderer renderer))
        {
            spawnPosition.y -= renderer.bounds.extents.y;
        }

        GameObject newObj = Instantiate(prefab, spawnPosition, Quaternion.identity);

        currentElems.Add(newObj);
    }

    private void MoveAndRemoveObjects()
    {
        for (int i = currentElems.Count - 1; i >= 0; i--)
        {
            GameObject elem = currentElems[i];
            if (elem == null) continue;

            elem.transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (elem.transform.position.z <= destroyZ)
            {
                Destroy(elem);
                currentElems.RemoveAt(i);
            }
        }
    }
}
