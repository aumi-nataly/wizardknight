using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SpawnerEnemy : MonoBehaviour
{
    private EnemyFactory enemyFactory;  

    [SerializeField]
    private EnemyType spawnType;

    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public int SpawnerId;

    private GameObject enemy;

    private Enemy concreteEnemy;
    private bool _isEnemyDead;

    private WorldStateManager _worldStateManager;
    private AudioManager _audioManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager, EnemyFactory enemyFact, AudioManager audioManager)
    {
        _worldStateManager = worldStateManager;
        enemyFactory = enemyFact;
        _audioManager = audioManager;
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
        if (_worldStateManager.IsDeadEnemy(SpawnerId))
        { return; }

        enemy = enemyFactory.GetEnemyFromPool(spawnType);
        enemy.transform.position = transform.position;
      
        concreteEnemy = enemy.GetComponent<Enemy>();
        concreteEnemy.Construct(_audioManager);

        _isEnemyDead = false;

    }

    private void DespawnEnemy()
    {
        enemyFactory.ReturnEnemyToPool(spawnType,enemy);
        enemy = null;
        concreteEnemy = null;
    }


    public void MainTick()
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
        _worldStateManager.AddDeathEnemy(SpawnerId);
        DespawnEnemy();
    }
}
