using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyFactory enemyFactory;  

    [SerializeField]
    private EnemyType spawnType;

    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int SpawnerId;

    private GameObject enemy;

    private Enemy concreteEnemy;
    private bool _isEnemyDead;

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

        concreteEnemy = enemy.GetComponent<Enemy>();
        _isEnemyDead = false;

    }

    private void DespawnEnemy()
    {
        enemyFactory.ReturnEnemyToPool(spawnType,enemy);
        enemy = null;
        concreteEnemy = null;
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

        if (concreteEnemy != null && concreteEnemy.IsReadyToDespawn())
        {
            UpdateDeadEnemy();
            return;
        }
    }

    
    private void UpdateDeadEnemy()
    {
        if (_isEnemyDead) return; // Уже обработан

        _isEnemyDead = true;
        WorldStateManager.Instance.AddDeathEnemy(SpawnerId);
        DespawnEnemy();
        Debug.Log("умер проивник номер "+ SpawnerId);
    }
}
