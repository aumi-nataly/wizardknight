using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class SpawnerBonusManager : IStartable, ITickable
{
    private readonly BonusFactory bonusFactory;
    private readonly WorldStateManager worldStateManager;
    private SpawnerBonus[] _spawners;
    private AudioManager _audioManager;

    public SpawnerBonusManager(BonusFactory factory, WorldStateManager wsm, AudioManager audioManager)
    {
        bonusFactory = factory;
        worldStateManager = wsm;
        _audioManager = audioManager;
    }
    public void Start()
    {
        _spawners = Object.FindObjectsOfType<SpawnerBonus>();

        foreach (var spawner in _spawners)
        {
            spawner.Construct(worldStateManager, bonusFactory, _audioManager);
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
