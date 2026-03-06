using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<WorldStateManager>();
        builder.RegisterComponentInHierarchy<AudioManager>();
        builder.RegisterComponentInHierarchy<LevelManager>();
        
       
        Debug.Log("WorldStateManager зарегистрирован в контейнере");
    }
}
