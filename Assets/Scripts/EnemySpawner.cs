using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyPrefab;

    [SerializeField]
    private List<Sprite> spriteSheets;

    private void Start()
    {
        SpawnController.instance.AddSpawner(this);
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.SetSprites(spriteSheets[Random.Range(0, spriteSheets.Count)]);
    }
}
