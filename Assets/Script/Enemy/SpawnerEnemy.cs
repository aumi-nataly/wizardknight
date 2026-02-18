using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyFactory enemyFactory;  // Ссылка на фабрику

    [SerializeField]
    private EnemyType spawnType;

    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int SpawnerId;

    private GameObject enemy;


    // Метод для спавна врага
    private void SpawnEnemy()
    {
        if (enemyFactory == null)
        {
            Debug.LogError("EnemyFactory не назначена!");
            return;
        }

        //не отображать мертвого врага, пока мир не перезагрузится
        if (WorldStateManager.Instance.IsDeadEnemy(SpawnerId))
        { return; }

        enemy = enemyFactory.GetEnemyFromPool(spawnType);
        enemy.transform.position = transform.position;

    }

    private void DespawnEnemy()
    {
        enemyFactory.ReturnEnemyToPool(spawnType,enemy);
        enemy = null;
    }


    void Update()
    { 
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist < spawnDistance && enemy == null)
        {
            SpawnEnemy();
            return;
        }

        if (dist > spawnDistance * 2f && enemy != null)
        {
            DespawnEnemy();
            return;
        }
    }
}
