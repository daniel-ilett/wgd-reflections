using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private List<EnemySpawner> spawners = new List<EnemySpawner>();

    [SerializeField]
    private float frequency = 1.0f;

    private float timer = 0.0f;

    public static SpawnController instance = null;

    // Register a singleton.
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > frequency)
        {
            // Tell a spawner to create an enemy.
            int rand = Random.Range(0, spawners.Count);
            spawners[rand].SpawnEnemy();
            timer = 0.0f;
        }
    }

    public void AddSpawner(EnemySpawner newSpawner)
    {
        spawners.Add(newSpawner);
    }
}
