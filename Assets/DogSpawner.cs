using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : MonoBehaviour
{
    public List<GameObject> GameObjects;

    private float _lastSpawnTime;
    public float SpawnTime = 0.5f;
    
    void Update()
    {
        if (Time.time - _lastSpawnTime > SpawnTime)
        {
            var instance = Instantiate(GameObjects[Random.Range(0, GameObjects.Count)], transform.position + new Vector3(Random.Range(-10f, 10f), 0, 0), Random.rotation);
            instance.transform.localScale = Vector3.one * 0.5f;
            _lastSpawnTime = Time.time;
        }
    }
}
