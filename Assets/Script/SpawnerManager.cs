using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

public class SpawnerManager :IStartable, ITickable
{
    private readonly EnemyFactory enemyFactory;
    private readonly WorldStateManager worldStateManager;
    private  SpawnerEnemy[] _spawners;

    public SpawnerManager(EnemyFactory factory, WorldStateManager wsm)
    {
        enemyFactory = factory;
        worldStateManager = wsm;
    }
    public void Start()
    {
        _spawners = Object.FindObjectsOfType<SpawnerEnemy>();
       
        foreach (var spawner in _spawners)
        {
            spawner.Construct(worldStateManager, enemyFactory);
            spawner.MainTick();
        }
    }


public void Tick()
{
    foreach (var spawner in _spawners)
    {
        spawner.MainTick();
    }
}

}
