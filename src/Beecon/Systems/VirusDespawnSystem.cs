using Beecon.Components;

namespace Beecon.Systems;

public sealed class VirusDespawnSystem : GameSystem
{
    public static float DespawnDistance => 1200;

    public override void Update()
    {
        var target = Scene.Camera.Target;
        var maxDistanceSquared = DespawnDistance * DespawnDistance;
        foreach (var (entity, _, body) in Entries<Virus, Body>())
            if ((body.Position - target).LengthSquared() > maxDistanceSquared)
                entity.Destroy();
    }
}
