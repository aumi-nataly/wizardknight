using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<EnemyFactory>();
        builder.RegisterComponentInHierarchy<BonusFactory>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<ExitLevel>();
        builder.RegisterComponentInHierarchy<PauseManager>();
        builder.RegisterComponentInHierarchy<PlayerManager>();
        builder.RegisterComponentInHierarchy<LifeUI>();
        builder.RegisterComponentInHierarchy<ScreenGameUI>();
        builder.RegisterComponentInHierarchy<RegenerationTree>();
        builder.RegisterComponentInHierarchy<EmptyFloor>();
        builder.RegisterEntryPoint<SpawnerManager>();
        builder.RegisterEntryPoint<SpawnerBonusManager>();

    }


}
