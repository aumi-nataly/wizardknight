using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{

    //[SerializeField] private SpawnerEnemy[] spawnerEnemies;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<EnemyFactory>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<ExitLevel>();
        builder.RegisterComponentInHierarchy<PauseManager>();
        builder.RegisterComponentInHierarchy<PlayerManager>();
        builder.RegisterComponentInHierarchy<LifeUI>();
        builder.RegisterComponentInHierarchy<ScreenGameUI>();
        builder.RegisterComponentInHierarchy<RegenerationTree>();

        //  builder.RegisterComponentInHierarchy<SpawnerEnemy>().AsSelf().AsImplementedInterfaces(); //не исправил ошибку
        // builder.RegisterComponentInHierarchy<SpawnerEnemy>().As<IStartable>().As<ITickable>(); //не исправил ошибку



        //var spawners = FindObjectsOfType<SpawnerEnemy>();
        //Debug.Log($"Найдено SpawnerEnemy на сцене: {spawners.Length}");

      //  var sp = GetComponentsInChildren<SpawnerEnemy>(true);
        // builder.RegisterInstance(sp);
      //  builder.RegisterInstance(sp.ToList());

        //Debug.Log($"Найдено GetComponentsInChildren на сцене: {sp.Length}");

       // builder.RegisterComponentInHierarchy<SpawnerEnemy>();
        //  builder.Register<SpawnerManager>(Lifetime.Scoped).As<ITickable>();
        builder.RegisterEntryPoint<SpawnerManager>();


    }


}
