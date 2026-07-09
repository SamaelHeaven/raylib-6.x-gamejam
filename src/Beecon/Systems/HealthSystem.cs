using Beecon.Components;

namespace Beecon.Systems;

public sealed class HealthSystem : GameSystem
{
    public override void PostUpdate()
    {
        foreach (var (entity, health) in Entries<Health>())
            if (health.IsDead)
                entity.Destroy();
    }
}
