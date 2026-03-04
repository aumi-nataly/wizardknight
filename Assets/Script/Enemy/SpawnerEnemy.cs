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
    private bool _canWork;

    private void Start()
    {
        WorldStateManager.Instance.OnLoadedWorldState += HandleStarted;
    }

    private void OnDestroy()
    {
        if (WorldStateManager.Instance != null)
            WorldStateManager.Instance.OnLoadedWorldState -= HandleStarted;
    }

    private void HandleStarted()
    {
        _canWork = true;
    }




    /// <summary>
    /// Метод для спавна врага
    /// </summary>
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
        if (!_canWork)
            return;

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
