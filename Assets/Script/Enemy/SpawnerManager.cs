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
    private AudioManager _audioManager;

    public SpawnerManager(EnemyFactory factory, WorldStateManager wsm, AudioManager audioManager)
    {
        enemyFactory = factory;
        worldStateManager = wsm;
        _audioManager = audioManager;
    }
    public void Start()
    {
        _spawners = Object.FindObjectsOfType<SpawnerEnemy>();
       
        foreach (var spawner in _spawners)
        {
            spawner.Construct(worldStateManager, enemyFactory, _audioManager);
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
