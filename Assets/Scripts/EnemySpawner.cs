using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    [SerializeField]
    private List<RuntimeAnimatorController> controllers;

    private void Start()
    {
        SpawnController.instance.AddSpawner(this);
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.SetController(controllers[Random.Range(0, controllers.Count)]);
    }
}
