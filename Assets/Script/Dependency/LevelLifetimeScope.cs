using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<ExitLevel>();
        builder.RegisterComponentInHierarchy<PauseManager>();
    }
}
