using Beecon.Components;

namespace Beecon.Systems;

public sealed class VirusDespawnSystem : GameSystem
{
    public override void Update()
    {
        var target = Scene.Camera.Target;
        var maxDistanceSquared = Gameplay.Virus.DespawnDistance * Gameplay.Virus.DespawnDistance;
        foreach (var (entity, _, body) in Entries<Virus, Body>())
            if (
                !entity.TryGet(out Boss _)
                && (body.Position - target).LengthSquared() > maxDistanceSquared
            )
                entity.Destroy();
    }
}
